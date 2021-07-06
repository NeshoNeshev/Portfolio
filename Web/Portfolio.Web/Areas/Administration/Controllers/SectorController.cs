using System;

namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.PositionViewModel;
    using Portfolio.Web.ViewModels.Administration.SectorViewModels;

    public class SectorController : AdministrationController
    {
        private readonly ICreateSectorService createSector;
        private readonly ICreateOrganizationServices organizationServices;
        private readonly ICreatePositionService positionService;
        private readonly IDeletableEntityRepository<Sector> sectoRepository;

        public SectorController(ICreateSectorService createSector, ICreateOrganizationServices organizationServices, ICreatePositionService positionService, IDeletableEntityRepository<Sector> sectoRepository)
        {
            this.createSector = createSector;
            this.organizationServices = organizationServices;
            this.positionService = positionService;
            this.sectoRepository = sectoRepository;
        }

        public IActionResult AllSectors()
        {
            var viewModel = new AllSectorsViewModel();
            var model = this.sectoRepository.All().To<SectorViewModel>().ToList();
            viewModel.Sector = model;
            return this.View(viewModel);
        }

        public IActionResult CreateSector()
        {

            return this.View();

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSector(CreateSectorInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                var sectorName = this.createSector.FindByNameAsync(model.SectorName);
                var organization = this.organizationServices.FindByNameAsync(model.OrganizationName);
                if (sectorName)
                {
                    this.ModelState.AddModelError(nameof(CreateSectorInputModel.SectorName), $"Exist {model.SectorName}");
                    return this.View(model);
                }

                if (!organization)
                {
                    this.ModelState.AddModelError(nameof(CreateSectorInputModel.OrganizationName), $"Not Exist {model.OrganizationName}");
                    return this.View(model);
                }

                try
                {
                    await this.createSector.CreateAsync(model);
                }
                catch (Exception e)
                {
                    return this.View(model);
                }
            }

            return this.RedirectToAction("AllSectors");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditSectorInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                var exist = this.createSector.FindByNameAsync(model.SectorName);

                if (!exist)
                {
                    this.ModelState.AddModelError(nameof(EditSectorInputModel.SectorName), $"Not Exist {model.SectorName}");
                    return this.View(model);
                }
            }
            await this.createSector.UpdateAsync(model);

            return this.RedirectToAction("AllSectors");
        }
    }
}
