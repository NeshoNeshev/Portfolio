using Portfolio.Web.ViewModels.Administration.PositionViewModel;

namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class PositionController : AdministrationController
    {
        private readonly ISectorService sectorService;
        private readonly IPositionService positionService;
        private readonly IEnumerable<SectorDropDownViewModel> sectorDropDown;
        private readonly IEnumerable<PositionDropDownViewModel> positionDropDown;

        public PositionController(ISectorService sectorService, IPositionService positionService)
        {
            this.sectorService = sectorService;
            this.positionService = positionService;
            this.positionDropDown = this.positionService.GetAll<PositionDropDownViewModel>();
            this.sectorDropDown = this.sectorService.GetAll<SectorDropDownViewModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreatePosition() => this.View(new CreatePositionInputModel { SectorDropDown = this.sectorDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePosition(CreatePositionInputModel positionModel)
        {
            var positionName = this.positionService.FindByNameAsync(positionModel.PositionName);
            if (positionName)
            {
                positionModel.SectorDropDown = this.sectorDropDown.ToList();
                this.ModelState.AddModelError(nameof(CreatePositionInputModel.PositionName), $"Exist {positionModel.PositionName}");
                return this.View(positionModel);
            }

            if (!this.ModelState.IsValid)
            {
                positionModel.SectorDropDown = this.sectorDropDown.ToList();
                return this.View(positionModel);
            }

            await this.positionService.CreateAsync(positionModel);

            return this.RedirectToAction("AllPosition");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditPositionInputModel { DropDownModel = this.positionDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditPositionInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.DropDownModel = this.positionDropDown.ToList();
                return this.View(model);
            }

            await this.positionService.UpdateAsync(model);

            return this.RedirectToAction("AllPosition");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllPosition()
        {
            var model = this.positionService.GetAll<PositionViewModel>();
            var viewModel = new AllPositionViewModel() { PositionViewModels = model };
            return this.View(viewModel);
        }
    }
}
