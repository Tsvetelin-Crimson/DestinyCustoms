namespace DestinyCustoms.Web.Areas.Administration.Controllers
{
    using DestinyCustoms.Common;
    using DestinyCustoms.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
