namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Web.Areas.Administration.Views.Organization;
    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.Organization;

    public class OrganizationController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privateInformation;
        private readonly ICreateOrganizationServices organizationServices;

        public OrganizationController(IDeletableEntityRepository<Organization> organizationRepository,
            IDeletableEntityRepository<PrivateInformation> privateInformation,
            ICreateOrganizationServices organizationServices)
        {
            this.organizationRepository = organizationRepository;
            this.privateInformation = privateInformation;
            this.organizationServices = organizationServices;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateOrganization()
        {

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrganization(OrganizationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            else
            {
                var organizationName = this.organizationRepository.All()
                    .Any(x => x.OrganizationName == input.OrganizationName);
                var privateName = this.privateInformation.All().Any(x => x.FirstName == input.PrivateName);
                if (organizationName)
                {
                    this.ModelState.AddModelError(
                        nameof(OrganizationInputModel.OrganizationName),
                        $"Exist {input.OrganizationName}");
                    return this.View(input);
                }

                if (!privateName)
                {
                    this.ModelState.AddModelError(
                        nameof(OrganizationInputModel.PrivateName),
                        $"Not Found {input.PrivateName}");
                    return this.View(input);
                }
            }

            await this.organizationServices.CreateAsync(input);
            return this.View(input);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var organizations = this.organizationServices.GetAll<OrganizationDropDownViewModel>().ToList();
            var viewModel = new EditOrganizationInputModel();
            viewModel.OrganizaztionDropDown = organizations;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditOrganizationInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.organizationServices.UpdateAsync(model);

            return this.Json("Sucsess");
        }
    }
}
