namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Certificate;
    using Portfolio.Web.ViewModels.Administration.Course;

    public class CertificateController : AdministrationController
    {
        private readonly ICreateCourseService courseService;
        private readonly ICreateCertificatesService certificatesService;

        public CertificateController(ICreateCourseService courseService, ICreateCertificatesService certificatesService)
        {
            this.courseService = courseService;
            this.certificatesService = certificatesService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateCertificate()
        {
            var courses = this.courseService.GetAll<CourseDropDown>().ToList();
            var viewModel = new CertificateInputModel();
            viewModel.CourseDropDowns = courses;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCertificate(CertificateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                var certificateName = this.courseService.FindByNameAsync(model.CertificateName);
                if (certificateName)
                {
                    this.ModelState.AddModelError(nameof(CertificateInputModel.CertificateName), $"Exist {model.CertificateName}");
                    return this.View(model);
                }

                await this.certificatesService.CreateAsync(model);
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCertificate()
        {
            var sectors = this.certificatesService.GetAll<CertificateDropDown>().ToList();
            var viewModel = new EditCertificateInputModel();
            viewModel.CertificateDropDowns = sectors;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCertificate(EditCertificateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.certificatesService.UpdateAsync(model);

            return this.Json("asdsa");
        }
    }
}
