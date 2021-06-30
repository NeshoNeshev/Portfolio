using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Common.Repositories;
using Portfolio.Data.Models;
using Portfolio.Services.Mapping;
using Portfolio.Web.ViewModels.PrivateInformationViewModel;

namespace Portfolio.Web.Controllers
{
    public class AboutController : BaseController
    {
        private readonly IDeletableEntityRepository<PrivateInformation> privateRepository;

        public AboutController(IDeletableEntityRepository<PrivateInformation> privateRepository)
        {
            this.privateRepository = privateRepository;
        }

        public IActionResult About()
        {
            var viewModel = new AboutModel();
            var model = this.privateRepository.All().To<PrivateInformationViewModel>().ToList();
            viewModel.privateInformation = model;
            return this.View(viewModel);
        }
    }
}
