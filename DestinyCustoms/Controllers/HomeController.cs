using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DestinyCustoms.Models;
using DestinyCustoms.Models.Home;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Armors;

namespace DestinyCustoms.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeaponsService weaponsService;
        private readonly IArmorsService armorsService;

        public HomeController(
            IWeaponsService weaponsService, 
            IArmorsService armorsService)
        {
            this.weaponsService = weaponsService;
            this.armorsService = armorsService;
        }

        public IActionResult Index()
            => View(new HomeViewModel
            {
                Weapons = this.weaponsService.MostRecentlyCreated(),
                Armors = this.armorsService.MostRecentlyCreated()
            });


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
