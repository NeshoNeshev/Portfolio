namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Speciality;
    using Portfolio.Web.ViewModels.Administration.University;

    public class SpecialtyController : AdministrationController
    {
        private readonly ICreateUniversityService _universityService;
        private readonly ICreateSpecialtiesService _createSpecialtiesService;

        public SpecialtyController(ICreateUniversityService universityService,ICreateSpecialtiesService createSpecialtiesService)
        {
            _universityService = universityService;
            _createSpecialtiesService = createSpecialtiesService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateSpecialty()
        {
            var universities = this._universityService.GetAll<UniversityDropDownViewModel>().ToList();
            var viewModel = new CreateSpecialtyInputModel();
            viewModel.UniversityDropDown = universities;
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSpecialty(CreateSpecialtyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                var sectorName = this._createSpecialtiesService.FindByNameAsync(model.SpecialtyName);
                var university = this._universityService.FindByIdAsync(model.UniversityId);
                if (sectorName)
                {
                    this.ModelState.AddModelError(nameof(CreateSpecialtyInputModel.SpecialtyName), $"Exist {model.SpecialtyName}");
                    return this.View(model);
                }

                await this._createSpecialtiesService.CreateAsync(model);
            }

            return this.Json("sadasda");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var sectors = this._createSpecialtiesService.GetAll<SpecialityDropDown>().ToList();
            var viewModel = new EditSpecialityInputModel();
            viewModel.SpecialityDropDowns = sectors;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditSpecialityInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            await this._createSpecialtiesService.UpdateAsync(model);

            return this.Json("sadasda");
        }
    }
}
