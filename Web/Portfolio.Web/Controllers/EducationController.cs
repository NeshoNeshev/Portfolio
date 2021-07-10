namespace Portfolio.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.EducationViewModels;

    public class EducationController : Controller
    {
        private readonly IUniversityService createUniversity;

        public EducationController(IUniversityService createUniversity)
        {
            this.createUniversity = createUniversity;
        }

        public IActionResult Education()
        {
            var viewModel = new AllEducationsViewModel();
            var model = this.createUniversity.GetAll<EducationViewModel>().ToList();
            viewModel.EducationViewModels = model;

            return this.View(viewModel);
        }
    }
}
