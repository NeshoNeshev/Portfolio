namespace Portfolio.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.CertificateViewModels;

    public class CertificateController : Controller
    {
        private readonly ICertificatesService certificateService;

        public CertificateController(ICertificatesService certificatesService)
        {
            this.certificateService = certificatesService;
        }

        public IActionResult Certificate()
        {
            var viewModel = new CertificateModel();
            var model = this.certificateService.GetAll<CertificateViewModel>().ToList();
            viewModel.Certificates = model;
            return this.View(viewModel);
        }
    }
}
