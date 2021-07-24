using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DestinyCustoms.Models;
using DestinyCustoms.Models.Home;
using DestinyCustoms.Services.Weapons;

namespace DestinyCustoms.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeaponsService weaponsService;

        public HomeController(IWeaponsService weaponsService)
            => this.weaponsService = weaponsService;

        public IActionResult Index() 
            => View(new HomeViewModel { Weapons = this.weaponsService.MostRecentlyCreated() });


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
