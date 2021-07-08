using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Services.Data;
using Portfolio.Web.ViewModels.CertificateViewModels;

namespace Portfolio.Web.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ICreateCertificatesService _certificatesService;

        public CertificateController(ICreateCertificatesService certificatesService)
        {
            _certificatesService = certificatesService;
        }
        public IActionResult Certificate()
        {
            var viewModel = new CertificateModel();
            var model = this._certificatesService.GetAll<CertificateViewModel>().ToList();
            viewModel.Certificates = model;
            return View(viewModel);
        }
    }
}
