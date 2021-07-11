namespace Portfolio.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Dashboard;
    using Portfolio.Web.ViewModels.Administration.Organization;
    using Portfolio.Web.ViewModels.OrganizationViewModels;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class OrganizationController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<PrivateInformation> privateInformation;
        private readonly IOrganizationServices organizationServices;
        private readonly IEnumerable<OrganizationDropDownViewModel> organizationDropDown;

        public OrganizationController(IDeletableEntityRepository<Organization> organizationRepository,
            IDeletableEntityRepository<PrivateInformation> privateInformation,
            IOrganizationServices organizationServices)
        {
            this.organizationRepository = organizationRepository;
            this.privateInformation = privateInformation;
            this.organizationServices = organizationServices;
            this.organizationDropDown = this.organizationServices.GetAll<OrganizationDropDownViewModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateOrganization() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrganization(OrganizationInputModel input)
        {
            var organizationName = this.organizationServices.FindByNameAsync(input.OrganizationName);
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

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.organizationServices.CreateAsync(input);
            return this.RedirectToAction("AllOrganization");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit() => this.View(new EditOrganizationInputModel{OrganizaztionDropDown = this.organizationDropDown.ToList()});

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditOrganizationInputModel model)
        {
            var organizationName = this.organizationServices.FindByIdAsync(model.NewOrganizationName);
            if (organizationName)
            {
                model.OrganizaztionDropDown = this.organizationDropDown.ToList();
                this.ModelState.AddModelError(
                    nameof(EditOrganizationInputModel.NewOrganizationName),
                    $"Exist {model.NewOrganizationName}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.OrganizaztionDropDown = this.organizationDropDown.ToList();
                return this.View(model);
            }

            await this.organizationServices.UpdateAsync(model);

            return this.RedirectToAction("AllOrganization");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllOrganization()
        {
            var model = this.organizationServices.GetAll<OrganizationViewModel>();
            var viewModel = new AllOrganizationViewModel() { OrganizationViewModels = model };
            return this.View(viewModel);
        }
    }
}
