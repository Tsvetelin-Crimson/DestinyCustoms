using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Models.Weapons;

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

        public IActionResult All()
        {
            var weapons = db.Exotics.Select(e =>new AllWeaponsViewModel
            {
                Id = e.Id,
                Name = e.Name,
                ClassName = e.WeaponClass.Name,
            });

            return View(weapons);
        }

        public IActionResult Details(int id)
        {
            var weapon = db.Exotics
                .Where(e => e.Id == id)
                .Select(e => new DetailsWeaponViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    IntrinsicName = e.WeaponIntrinsicName,
                    IntrinsicDescription = e.WeaponIntrinsicDescription,
                    CatalystName = e.CatalystName,
                    CatalystCompletionRequirement = e.CatalystCompletionRequirement,
                    ClassName = e.WeaponClass.Name,
                })
                .FirstOrDefault();
            //TODO: Remove unneeded classes in View
            //TODO: Ask for opinions on the Views
            return weapon != null ? View(weapon) : NotFound();
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
