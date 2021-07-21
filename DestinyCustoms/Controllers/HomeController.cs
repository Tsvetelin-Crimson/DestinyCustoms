using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DestinyCustoms.Models;
using DestinyCustoms.Data;
using System.Linq;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Models.Home;

namespace DestinyCustoms.Controllers
{
    public class HomeController : Controller
    {
        private readonly DestinyCustomsDbContext db;

        public HomeController(DestinyCustomsDbContext db)
            => this.db = db;

        public IActionResult Index()
        {
            var weapons = db.Weapons.Select(w => new AllWeaponsViewModel
            {
                Id = w.Id,
                Name = w.Name,
                ClassName = w.WeaponClass.Name,
            })
            .Take(4)
            .ToList();

            return View(new HomeViewModel { Weapons = weapons });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
