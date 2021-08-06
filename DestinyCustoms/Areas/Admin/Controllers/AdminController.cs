using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DestinyCustoms.Areas.Admin.Controllers
{
    using static Common.WebConstants;

    [Area(adminAreaName)]
    [Authorize(Roles = adminRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
