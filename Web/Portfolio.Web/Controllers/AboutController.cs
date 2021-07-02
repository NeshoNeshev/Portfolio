namespace Portfolio.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Services.Mapping;
    using Portfolio.Web.ViewModels.PrivateInformationViewModel;

    public class AboutController : BaseController
    {
        private readonly IDeletableEntityRepository<PrivateInformation> privateRepository;
        private readonly IGetAgeService privateInformationServices;

        public AboutController(IDeletableEntityRepository<PrivateInformation> privateRepository, IGetAgeService privateInformationServices)
        {
            this.privateRepository = privateRepository;
            this.privateInformationServices = privateInformationServices;
        }

        public IActionResult About()
        {
            var viewModel = new AboutModel();
            var model = this.privateRepository.All().To<PrivateInformationViewModel>().ToList();

            model.Where(w => w.Age == null).ToList().ForEach(s => s.Age = this.privateInformationServices.GetAge(s.Birthday));

            viewModel.privateInformation = model;

            return this.View(viewModel);
        }
    }
}
