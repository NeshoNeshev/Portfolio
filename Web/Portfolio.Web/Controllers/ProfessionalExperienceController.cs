using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Services.Data;
using Portfolio.Web.ViewModels.ProfessionalExperienceViewModels;

namespace Portfolio.Web.Controllers
{
    public class ProfessionalExperienceController : Controller
    {
        private readonly ICreateOrganizationServices _organizationServices;

        public ProfessionalExperienceController(ICreateOrganizationServices organizationServices)
        {
            _organizationServices = organizationServices;
        }

        public IActionResult ProfessionalExperience()
        {
            var model = new AllProfessionalExperience();
            var viewModel = this._organizationServices.GetAll<ProfessionalExperienceViewModel>().ToList();
            model.ProfessionalExperienceViewModels = viewModel;
            return View(model);
        }
    }
}
