namespace Portfolio.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Common;
    using Portfolio.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
