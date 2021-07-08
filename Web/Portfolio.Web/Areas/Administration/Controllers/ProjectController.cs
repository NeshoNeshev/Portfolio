using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;
using Portfolio.Services.Data;
using Portfolio.Web.CloudinaryHelper;
using Portfolio.Web.ViewModels.Administration.Project;

namespace Portfolio.Web.Areas.Administration.Controllers
{
    public class ProjectController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Project> _projectRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> _privateInformation;
        private readonly IProjectService _projectService;
        private readonly Cloudinary cloudinary;

        public ProjectController(Cloudinary cloudinary, IDeletableEntityRepository<Project> projectRepository, IDeletableEntityRepository<PrivateInformation> privateInformation,IProjectService projectService)
        {
            _projectRepository = projectRepository;
            _privateInformation = privateInformation;
            _projectService = projectService;
            cloudinary = cloudinary;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateProject()
        {
            return this.View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProject(ICollection<IFormFile> files, ProjectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                var projectName = this._projectRepository.All()
                    .Any(x => x.ProjectName == model.ProjectName);
                var privateName = this._privateInformation.All().Any(x => x.FirstName == model.PrivateName);
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
            }

            var result =  await CloudinaryExtension.UploadAsync(this.cloudinary, files);

            await this._projectService.CreateAsync(model);
            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            var projects = this._projectService.GetAll<ProjectDropDownViewModel>().ToList();
            var viewModel = new EditProjectInputModel();
            viewModel.ProjectDropDown = projects;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditProjectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this._projectService.UpdateAsync(model);

            return this.Json("Sucsess");
        }
    }
}
