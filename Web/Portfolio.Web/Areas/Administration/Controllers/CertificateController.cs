namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Certificate;
    using Portfolio.Web.ViewModels.Administration.Course;
    using Portfolio.Web.ViewModels.CertificateViewModels;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CertificateController : AdministrationController
    {
        private readonly ICourseService courseService;
        private readonly ICertificatesService certificatesService;
        private readonly IEnumerable<CourseDropDown> courseDropDowns;
        private readonly IEnumerable<CertificateDropDown> certificateDropDowns;

        public CertificateController(ICourseService courseService, ICertificatesService certificatesService)
        {
            this.courseService = courseService;
            this.certificatesService = certificatesService;
            this.courseDropDowns = this.courseService.GetAll<CourseDropDown>();
            this.certificateDropDowns = this.certificatesService.GetAll<CertificateDropDown>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateCertificate() => this.View(new CertificateInputModel { CourseDropDowns = this.courseDropDowns.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCertificate(CertificateInputModel model)
        {
            var certificateName = this.certificatesService.FindByName(model.CertificateName);
            if (certificateName)
            {
                model.CourseDropDowns = this.courseDropDowns.ToList();
                this.ModelState.AddModelError(nameof(CertificateInputModel.CertificateName), $"Exist {model.CertificateName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.CourseDropDowns = this.courseDropDowns.ToList();
                return this.View(model);
            }

            await this.certificatesService.CreateAsync(model);

            return this.RedirectToAction("AllCertificates");
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCertificate() => this.View(new EditCertificateInputModel{CertificateDropDowns = this.certificateDropDowns.ToList()});

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCertificate(EditCertificateInputModel model)
        {
            var certificateName = this.certificatesService.FindByName(model.NewCertificateName);
            if (certificateName)
            {
                model.CertificateDropDowns = this.certificateDropDowns.ToList();
                this.ModelState.AddModelError(nameof(EditCertificateInputModel.NewCertificateName), $"Exist {model.NewCertificateName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.CertificateDropDowns = this.certificateDropDowns.ToList();
                return this.View(model);
            }

            await this.certificatesService.UpdateAsync(model);

            return this.RedirectToAction("AllCertificates");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllCertificates()
        {
            var model = this.certificatesService.GetAll<CertificateViewModel>();
            var viewModel = new CertificateModel { Certificates = model };
            return this.View(viewModel);
        }
    }
}
