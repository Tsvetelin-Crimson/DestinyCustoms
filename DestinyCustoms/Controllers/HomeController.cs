using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using DestinyCustoms.Models;
using DestinyCustoms.Models.Home;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Weapons.Models;
using DestinyCustoms.Services.Armors;
using DestinyCustoms.Services.Armors.Models;
using System.Collections.Generic;

namespace DestinyCustoms.Controllers
{
    using static Common.WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IWeaponsService weaponsService;
        private readonly IArmorsService armorsService;
        private readonly IMemoryCache cache;

        public HomeController(
            IWeaponsService weaponsService,
            IArmorsService armorsService, 
            IMemoryCache cache)
        {
            this.weaponsService = weaponsService;
            this.armorsService = armorsService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var weapons = this.cache.Get<List<WeaponServiceModel>>(LatestWeaponsCacheKey);
            var armors = this.cache.Get<List<ArmorServiceModel>>(LatestArmorsCacheKey);

            if (weapons == null)
            {
                weapons = this.weaponsService.MostRecentlyCreated();
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                this.cache.Set(LatestWeaponsCacheKey, weapons, options);
            }

            if (armors == null)
            {
                armors = this.armorsService.MostRecentlyCreated();
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(20));

                this.cache.Set(LatestArmorsCacheKey, armors, options);
            }

            return View(new HomeViewModel
               {
                   Weapons = weapons,
                   Armors = armors,
               });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
