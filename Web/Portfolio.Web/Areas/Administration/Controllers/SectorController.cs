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
            var organizations = this.organizationServices.GetAll<OrganizationDropDownViewModel>().ToList();
            var viewModel = new CreateSectorInputModel();
            viewModel.OrganizationDropDown = organizations;
            return this.View(viewModel);

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
                var organization = this.organizationServices.FindByIdAsync(model.OrganizationId);
                if (sectorName)
                {
                    this.ModelState.AddModelError(nameof(CreateSectorInputModel.SectorName), $"Exist {model.SectorName}");
                    return this.View(model);
                }

                await this.createSector.CreateAsync(model);
            }

            return this.RedirectToAction("AllSectors");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var sectors = this.createSector.GetAll<SectorDropDownViewModel>().ToList();
            var viewModel = new EditSectorInputModel();
            viewModel.SectorDropDown = sectors;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditSectorInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            await this.createSector.UpdateAsync(model);

            return this.RedirectToAction("AllSectors");
        }
    }
}
