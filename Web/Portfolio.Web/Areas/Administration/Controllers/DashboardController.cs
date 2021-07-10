namespace Portfolio.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        public DashboardController()
        {
        }

        [Authorize]
        public IActionResult Index()
        {

            return this.View();
        }
    }
}
