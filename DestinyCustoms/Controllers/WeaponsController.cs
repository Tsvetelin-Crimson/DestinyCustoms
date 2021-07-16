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
                CatalystEffect = weapon.CatalystEffect,
                WeaponClassId = weapon.ClassId,
            };

            this.db.Weapons.Add(weaponData);
            this.db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All([FromQuery]AllWeaponsQueryModel query)
        {
            var weaponsQuery = db.Weapons.AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                weaponsQuery = weaponsQuery.Where(w => w.Name.Contains(query.SearchTerm));
            }

            if (!string.IsNullOrEmpty(query.WeaponType) && query.WeaponType != "All")
            {
                weaponsQuery = weaponsQuery.Where(w => w.WeaponClass.Name == query.WeaponType);
            }

            var weapons = weaponsQuery
                .OrderByDescending(w => w.Id)
                .Skip(query.WeaponsPerPage * (query.CurrentPage - 1))
                .Take(query.WeaponsPerPage)
                .Select(w => new AllWeaponsViewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    ClassName = w.WeaponClass.Name,
                })
                .ToList();

            query.Weapons = weapons;
            query.WeaponTypes = db.ItemClasses.Select(i => i.Name).ToList();
            query.AllWeapons = weaponsQuery.Count();

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var weapon = db.Weapons
                .Where(e => e.Id == id)
                .Select(e => new DetailsWeaponViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    IntrinsicName = e.WeaponIntrinsicName,
                    IntrinsicDescription = e.WeaponIntrinsicDescription,
                    CatalystName = e.CatalystName,
                    CatalystCompletionRequirement = e.CatalystCompletionRequirement,
                    CatalystEffect = e.CatalystEffect,
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
