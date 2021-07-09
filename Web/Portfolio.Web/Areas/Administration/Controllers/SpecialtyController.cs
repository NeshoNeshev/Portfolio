namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Speciality;
    using Portfolio.Web.ViewModels.Administration.University;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class SpecialtyController : AdministrationController
    {
        private readonly ICreateUniversityService universityService;
        private readonly ICreateSpecialtiesService createSpecialtiesService;
        private readonly IEnumerable<UniversityDropDownViewModel> universityDropDown;
        private readonly IEnumerable<SpecialityDropDown> specialityDropDowns;

        public SpecialtyController(ICreateUniversityService universityService, ICreateSpecialtiesService createSpecialtiesService)
        {
            this.universityService = universityService;
            this.createSpecialtiesService = createSpecialtiesService;
            this.specialityDropDowns = this.createSpecialtiesService.GetAll<SpecialityDropDown>();
            this.universityDropDown = this.universityService.GetAll<UniversityDropDownViewModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateSpecialty() => this.View(new CreateSpecialtyInputModel{UniversityDropDown = this.universityDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSpecialty(CreateSpecialtyInputModel model)
        {
            var sectorName = this.createSpecialtiesService.FindByNameAsync(model.SpecialtyName);
            if (sectorName)
            {
                this.ModelState.AddModelError(nameof(CreateSpecialtyInputModel.SpecialtyName), $"Exist {model.SpecialtyName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.UniversityDropDown = this.universityDropDown.ToList();
                return this.View(model);
            }

            await this.createSpecialtiesService.CreateAsync(model);

            return this.Json("sadasda");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditSpecialityInputModel{SpecialityDropDowns = this.specialityDropDowns.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditSpecialityInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.SpecialityDropDowns = this.specialityDropDowns.ToList();
                return this.View(model);
            }

            await this.createSpecialtiesService.UpdateAsync(model);

            return this.Json("sadasda");
        }
    }
}
