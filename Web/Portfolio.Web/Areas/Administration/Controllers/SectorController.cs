namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.Organization;
    using Portfolio.Web.ViewModels.Administration.SectorViewModels;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class SectorController : AdministrationController
    {
        private readonly ISectorService createSector;
        private readonly IOrganizationServices organizationServices;
        private readonly IEnumerable<SectorDropDownViewModel> sectorDropDown;
        private readonly IEnumerable<OrganizationDropDownViewModel> organizationDropDown;

        public SectorController(ISectorService createSector, IOrganizationServices organizationServices)
        {
            this.createSector = createSector;
            this.organizationServices = organizationServices;
            this.sectorDropDown = this.createSector.GetAll<SectorDropDownViewModel>();
            this.organizationDropDown = this.organizationServices.GetAll<OrganizationDropDownViewModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateSector() => this.View(new CreateSectorInputModel { OrganizationDropDown = this.organizationDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSector(CreateSectorInputModel model)
        {
            var sectorName = this.createSector.FindByNameAsync(model.SectorName);
            if (sectorName)
            {
                model.OrganizationDropDown = this.organizationDropDown.ToList();
                this.ModelState.AddModelError(nameof(CreateSectorInputModel.SectorName), $"Exist {model.SectorName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.OrganizationDropDown = this.organizationDropDown.ToList();
                return this.View(model);
            }

            await this.createSector.CreateAsync(model);

            return this.RedirectToAction("AllSectors");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditSectorInputModel { SectorDropDown = this.sectorDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditSectorInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.SectorDropDown = this.sectorDropDown.ToList();
                return this.View(model);
            }

            await this.createSector.UpdateAsync(model);

            return this.RedirectToAction("AllSectors");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllSectors()
        {
            var model = this.createSector.GetAll<SectorViewModel>();
            var viewModel = new AllSectorsViewModel
            {
                Sector = model,
            };
            return this.View(viewModel);
        }
    }
}
