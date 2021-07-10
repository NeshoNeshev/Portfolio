namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.University;

    public class UniversityController : AdministrationController
    {

        private readonly IDeletableEntityRepository<PrivateInformation> privateEntityRepository;
        private readonly IDeletableEntityRepository<University> universityRepository;
        private readonly IUniversityService universityService;
        private readonly IEnumerable<UniversityDropDownViewModel> universityDropDown;

        public UniversityController(IDeletableEntityRepository<PrivateInformation> privateEntityRepository, IDeletableEntityRepository<University> universityRepository, IUniversityService universityService)
        {
            this.privateEntityRepository = privateEntityRepository;
            this.universityRepository = universityRepository;
            this.universityService = universityService;
            this.universityDropDown = this.universityService.GetAll<UniversityDropDownViewModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateUniversity() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUniversity(CreateUniversityInputModel model)
        {
            var universityName = this.universityService.FindByNameAsync(model.UniversityName);
            var privateName = this.privateEntityRepository.All().Any(x => x.FirstName == model.PrivateName);
            if (universityName)
            {
                this.ModelState.AddModelError(nameof(CreateUniversityInputModel.UniversityName), $"Exist {model.UniversityName}");
                return this.View(model);
            }

            if (!privateName)
            {
                this.ModelState.AddModelError(nameof(CreateUniversityInputModel.PrivateName), $"Not Found {model.PrivateName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.universityService.CreateAsync(model);
            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditUniversityInputModel{UniversityDropDown = this.universityDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditUniversityInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.UniversityDropDown = this.universityDropDown.ToList();
                return this.View(model);
            }

            await this.universityService.UpdateAsync(model);

            return this.Json("Sucsess");
        }
    }
}
