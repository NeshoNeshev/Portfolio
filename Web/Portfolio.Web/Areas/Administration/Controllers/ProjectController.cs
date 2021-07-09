namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Web.CloudinaryHelper;
    using Portfolio.Web.ViewModels.Administration.Project;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class ProjectController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privateInformation;
        private readonly IProjectService projectService;
        private readonly Cloudinary cloudinary;
        private readonly IEnumerable<ProjectDropDownViewModel> projectDropDown;

        public ProjectController(Cloudinary cloudinary, IDeletableEntityRepository<Project> projectRepository, IDeletableEntityRepository<PrivateInformation> privateInformation,IProjectService projectService)
        {
            this.projectRepository = projectRepository;
            this.privateInformation = privateInformation;
            this.projectService = projectService;
            this.cloudinary = cloudinary;
            this.projectDropDown = this.projectService.GetAll<ProjectDropDownViewModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateProject() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProject(ICollection<IFormFile> files, ProjectInputModel model)
        {
            var projectName = this.projectRepository.All()
                .Any(x => x.ProjectName == model.ProjectName);
            var privateName = this.privateInformation.All().Any(x => x.FirstName == model.PrivateName);
            if (projectName)
            {
                this.ModelState.AddModelError(
                    nameof(ProjectInputModel.ProjectName),
                    $"Exist {model.ProjectName}");
                return this.View(model);
            }

            if (!privateName)
            {
                this.ModelState.AddModelError(
                    nameof(ProjectInputModel.PrivateName),
                    $"Not Found {model.PrivateName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await CloudinaryExtension.UploadAsync(this.cloudinary, files);

            await this.projectService.CreateAsync(model);
            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditProjectInputModel{ProjectDropDown = this.projectDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditProjectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.ProjectDropDown = this.projectDropDown.ToList();
                return this.View(model);
            }

            await this.projectService.UpdateAsync(model);

            return this.Json("Sucsess");
        }
    }
}
