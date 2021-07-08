using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;
using Portfolio.Services.Data;
using Portfolio.Web.ViewModels.EducationViewModels;

namespace Portfolio.Web.Controllers
{
    public class EducationController : Controller
    {
        private readonly ICreateUniversityService _createUniversity;
        private readonly IDeletableEntityRepository<University> _universityRepository;

        public EducationController(ICreateUniversityService createUniversity)
        {
            _createUniversity = createUniversity;
        }

        public IActionResult Education()
        {
            var viewModel = new AllEducationsViewModel();
            var model = _createUniversity.GetAll<EducationViewModel>().ToList();
            viewModel.EducationViewModels = model;

            return View(viewModel);
        }
    }
}
