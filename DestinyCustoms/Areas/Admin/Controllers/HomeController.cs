using Microsoft.AspNetCore.Mvc;
using DestinyCustoms.Controllers;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Services.Armors;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Areas.Admin.Models.Home;

namespace DestinyCustoms.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IWeaponsService weaponsService;
        private readonly IArmorsService armorsService;

        public HomeController(IWeaponsService weaponsService, IArmorsService armorsService)
        {
            this.weaponsService = weaponsService;
            this.armorsService = armorsService;
        }

        public IActionResult MostRecentWeapons() 
            => View(new MostRecentItemsViewModel 
            {
                Items = weaponsService.AdminMostRecentlyModified(),
                ItemLocation = nameof(WeaponsController).RemoveControllerFromString(),
            });

        public IActionResult MostRecentArmors()
            => View(new MostRecentItemsViewModel
            {
               Items = armorsService.AdminMostRecentlyModified(),
               ItemLocation = nameof(ArmorsController).RemoveControllerFromString(),
            });
    }
}
