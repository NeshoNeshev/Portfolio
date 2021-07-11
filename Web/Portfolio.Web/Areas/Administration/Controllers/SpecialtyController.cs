namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Specialty;
    using Portfolio.Web.ViewModels.Administration.University;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class SpecialtyController : AdministrationController
    {
        private readonly IUniversityService universityService;
        private readonly ISpecialtiesService createSpecialtiesService;
        private readonly IEnumerable<UniversityDropDownViewModel> universityDropDown;
        private readonly IEnumerable<SpecialtyDropDown> specialityDropDowns;

        public SpecialtyController(IUniversityService universityService, ISpecialtiesService createSpecialtiesService)
        {
            this.universityService = universityService;
            this.createSpecialtiesService = createSpecialtiesService;
            this.specialityDropDowns = this.createSpecialtiesService.GetAll<SpecialtyDropDown>();
            this.universityDropDown = this.universityService.GetAll<UniversityDropDownViewModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateSpecialty() => this.View(new CreateSpecialtyInputModel{UniversityDropDown = this.universityDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSpecialty(CreateSpecialtyInputModel model)
        {
            var specialtyName = this.createSpecialtiesService.FindByNameAsync(model.SpecialtyName);
            if (specialtyName)
            {
                model.UniversityDropDown = this.universityDropDown.ToList();
                this.ModelState.AddModelError(nameof(CreateSpecialtyInputModel.SpecialtyName), $"Exist {model.SpecialtyName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.UniversityDropDown = this.universityDropDown.ToList();
                return this.View(model);
            }

            await this.createSpecialtiesService.CreateAsync(model);

            return this.RedirectToAction("AllSpecialty");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditSpecialtyInputModel{SpecialtyDropDowns = this.specialityDropDowns.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditSpecialtyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.SpecialtyDropDowns = this.specialityDropDowns.ToList();
                return this.View(model);
            }

            await this.createSpecialtiesService.UpdateAsync(model);

            return this.RedirectToAction("AllSpecialty");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllSpecialty()
        {
            var model = this.createSpecialtiesService.GetAll<SpecialtyViewModel>();
            var viewModel = new AllSpecialtyViewModel()
            {
                SpecialtyViewModels = model,
            };
            return this.View(viewModel);
        }
    }
}
