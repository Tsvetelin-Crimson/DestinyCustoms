using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Models.Weapons;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DestinyCustoms.Controllers
{
    public class WeaponsController : Controller
    {
        private readonly DestinyCustomsDbContext db;

        public WeaponsController(DestinyCustomsDbContext db)
        {
            this.db = db;
        }

        public IActionResult Add()
            => View(new AddWeaponFormModel { Classes = this.getClasses() });

        [HttpPost]
        public IActionResult Add(AddWeaponFormModel weapon)
        {
            if (!this.db.ItemClasses.Any(c => c.Id == weapon.ClassId))
            {
                this.ModelState.AddModelError(nameof(weapon.ClassId), "Weapon class does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                weapon.Classes = getClasses();

                return View(weapon);
            }

            var weaponData = new ExoticWeapon
            {
                Name = weapon.Name,
                WeaponIntrinsicName = weapon.IntrinsicName,
                WeaponIntrinsicDescription = weapon.IntrinsicDescription,
                CatalystName = weapon.CatalystName,
                CatalystCompletionRequirement = weapon.CatalystCompletionRequirement,
                WeaponClassId = weapon.ClassId,
            };

            this.db.Exotics.Add(weaponData);
            this.db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<WeaponClassViewModel> getClasses()
            => db.ItemClasses
            .Select(x => new WeaponClassViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
            .ToList();
    }
}
