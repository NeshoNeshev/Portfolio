
using Portfolio.Services.Mapping;
using Portfolio.Web.ViewModels.Administration.PositionViewModel;

namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class PositionController : AdministrationController
    {
        private readonly ICreateOrganizationServices _organizationServices;
        private readonly ICreatePositionService _positionService;
        private readonly IDeletableEntityRepository<Position> _positionRepository;

        public PositionController(ICreateOrganizationServices organizationServices, ICreatePositionService positionService,IDeletableEntityRepository<Position> positionRepository)
        {
            _organizationServices = organizationServices;
            _positionService = positionService;
            _positionRepository = positionRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreatePosition()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePosition(CreatePositionInputModel positionModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(positionModel);
            }
            else
            {
                var privateName = this._positionRepository.All().Any(x => x.PositionName == positionModel.PositionName);
                if (privateName)
                {
                    this.ModelState.AddModelError(nameof(CreatePositionInputModel.PositionName), $"Exist {positionModel.PositionName}");
                    return this.View(positionModel);
                }
            }
            await this._positionService.CreateAsync(positionModel);

            return this.Json("sasa: as");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditPositionInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                var exist = this._positionService.FindByNameAsync(model.PositionName);

                if (!exist)
                {
                    this.ModelState.AddModelError(nameof(EditSectorInputModel.SectorName), $"Not Exist {model.PositionName}");
                    return this.View(model);
                }
            }
            await this._positionService.UpdateAsync(model);

            return this.View();
        }
        //Todo not work
        public IActionResult ViewAllPosition()
        {
            var viewModel = new AllPositionModel();
            var model = this._positionRepository.All().To<EditPositionViewModel>().ToList();
            viewModel.Positions = model;
            return this.View(viewModel);
        }
    }
}
