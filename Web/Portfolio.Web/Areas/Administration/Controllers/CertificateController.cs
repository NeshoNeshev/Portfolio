using System.Collections.Generic;
using Portfolio.Common;

namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Certificate;
    using Portfolio.Web.ViewModels.Administration.Course;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CertificateController : AdministrationController
    {
        private readonly ICreateCourseService courseService;
        private readonly ICreateCertificatesService certificatesService;
        private readonly IEnumerable<CourseDropDown> courseDropDowns;
        private readonly IEnumerable<CertificateDropDown> certificateDropDowns;

        public CertificateController(ICreateCourseService courseService, ICreateCertificatesService certificatesService)
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
            var certificateName = this.courseService.FindByNameAsync(model.CertificateName);
            if (certificateName)
            {
                this.ModelState.AddModelError(nameof(CertificateInputModel.CertificateName), $"Exist {model.CertificateName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.CourseDropDowns = this.courseDropDowns.ToList();
                return this.View(model);
            }

            await this.certificatesService.CreateAsync(model);

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCertificate() => this.View(new EditCertificateInputModel{CertificateDropDowns = this.certificateDropDowns.ToList()});

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCertificate(EditCertificateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CertificateDropDowns = this.certificateDropDowns.ToList();
                return this.View(model);
            }

            await this.certificatesService.UpdateAsync(model);

            return this.Json("asdsa");
        }
    }
}
