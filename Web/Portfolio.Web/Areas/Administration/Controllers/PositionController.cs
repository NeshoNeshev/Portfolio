
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
        private readonly ICreateSectorService _sectorService;
        private readonly ICreatePositionService _positionService;
        private readonly IDeletableEntityRepository<Position> _positionRepository;

        public PositionController( ICreateSectorService sectorService,ICreatePositionService positionService,IDeletableEntityRepository<Position> positionRepository)
        {
            _sectorService = sectorService;
            _positionService = positionService;
            _positionRepository = positionRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreatePosition()
        {
            var sectors = this._sectorService.GetAll<SectorDropDownViewModel>().ToList();
            var viewModel = new CreatePositionInputModel();
            viewModel.SectorDropDown = sectors;
            return this.View(viewModel);
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
                var positionName = this._positionService.FindByNameAsync(positionModel.PositionName);
                if (positionName)
                {
                    this.ModelState.AddModelError(nameof(CreatePositionInputModel.PositionName), $"Exist {positionModel.PositionName}");
                    return this.View(positionModel);
                }
            }
            await this._positionService.CreateAsync(positionModel);

            return this.Json("sasa: as");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var positions = this._positionService.GetAll<PositionDropDownViewModel>().ToList();
            var viewModel = new EditPositionInputModel();
            viewModel.DropDownModel = positions;
            return this.View(viewModel);
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
                var exist = this._positionService.FindByIdAsync(model.Id);

                if (!exist)
                {
                    this.ModelState.AddModelError(nameof(EditPositionInputModel.Id), $"Not Exist {model.Id}");
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
