namespace Portfolio.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.ProfessionalExperienceViewModels;

    public class ProfessionalExperienceController : Controller
    {
        private readonly IOrganizationServices organizationServices;

        public ProfessionalExperienceController(IOrganizationServices organizationServices)
        {
            this.organizationServices = organizationServices;
        }

        public IActionResult ProfessionalExperience()
        {
            var model = new AllProfessionalExperience();
            var viewModel = this.organizationServices.GetAll<ProfessionalExperienceViewModel>().ToList();
            model.ProfessionalExperienceViewModels = viewModel;
            return this.View(model);
        }
    }
}
