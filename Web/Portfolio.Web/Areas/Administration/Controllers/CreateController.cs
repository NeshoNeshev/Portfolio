using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Services.Data;
    using Portfolio.Web.ViewModels.Administration.Dashboard;

    public class CreateController : AdministrationController
    {
        private readonly ICreateSectorService createSector;
        private readonly ICreateOrganizationServices _organizationServices;

        public CreateController(ICreateSectorService createSector,ICreateOrganizationServices organizationServices)
        {
            this.createSector = createSector;
            _organizationServices = organizationServices;
        }

        public IActionResult CreateSector()
        {
            var organization = this._organizationServices.GetAll<OrganizationDropDownModel>().ToList();
            var viewModel = new CreateSectorViewModel();
            viewModel.OrganizationDrop = organization;
            return View(viewModel);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSector(CreateSectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return this.View(model);
            }
            
            await this.createSector.CreateAsync(model.SectorName,model.OrganizationName,model.PositionName,model.PositionMoreInformation, model.PositionPeriod);

            return this.Json("sasa: as");
        }
    }
}
