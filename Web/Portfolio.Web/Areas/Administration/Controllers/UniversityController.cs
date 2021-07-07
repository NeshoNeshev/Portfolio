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
    using Portfolio.Web.ViewModels.Administration.University;

    public class UniversityController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> _privateEntityRepository;
        private readonly IDeletableEntityRepository<University> universityRepository;
        private readonly ICreateUniversityService universityService;

        public UniversityController(IDeletableEntityRepository<Town> townRepository, IDeletableEntityRepository<PrivateInformation> privateEntityRepository, IDeletableEntityRepository<University> universityRepository, ICreateUniversityService universityService)
        {
            this.townRepository = townRepository;
            this._privateEntityRepository = privateEntityRepository;
            this.universityRepository = universityRepository;
            this.universityService = universityService;
        }


        public IActionResult CreateUniversity()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUniversity(CreateUniversityInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                ModelState.Remove(nameof(CreateUniversityInputModel.UniversityName));
                this.ModelState.AddModelError(nameof(CreateUniversityInputModel.UniversityName), $"University first Letter to upper");
                return this.View(model);
            }
            else
            {
                var townName = this.townRepository.All().Any(x => x.TownName == model.TownName);
                var universityName = this.universityRepository.All().Any(x => x.UniversityName == model.UniversityName);
                var privateName = this._privateEntityRepository.All().Any(x => x.FirstName == model.PrivateName);
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

            }

            await this.universityService.CreateAsync(model);
            return this.View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var universities = this.universityService.GetAll<UniversityDropDownViewModel>().ToList();
            var viewModel = new EditUniversityInputModel();
            viewModel.UniversityDropDown = universities;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditUniversityInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.universityService.UpdateAsync(model);

            return this.Json("Sucsess");
        }
    }
}
