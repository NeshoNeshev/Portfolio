namespace Portfolio.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.ProjectViewModels;

    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public IActionResult Project()
        {
            var viewModel = new AllProjectViewModel();
            var model = this.projectService.GetAll<ProjectViewModel>().ToList();
            viewModel.ProjectViewModels = model;
            return this.View(viewModel);
        }
    }
}
