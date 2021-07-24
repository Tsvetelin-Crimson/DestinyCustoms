using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Comments;

namespace DestinyCustoms.Controllers
{
    public class WeaponsController : Controller
    {
        private readonly IWeaponsService weaponsService;
        private readonly ICommentsService commentsService;


        public WeaponsController(
            IWeaponsService weaponsService, 
            ICommentsService commentsService)
        {
            this.weaponsService = weaponsService;
            this.commentsService = commentsService;
        }

        [Authorize]
        public IActionResult Add()
            => View(new AddWeaponFormModel { Classes = this.weaponsService.AllClasses() });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddWeaponFormModel weapon)
        {
            if (!this.weaponsService.WeaponClassExists(weapon.ClassId))
            {
                this.ModelState.AddModelError(nameof(weapon.ClassId), "Weapon class does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                weapon.Classes = this.weaponsService.AllClasses();
                return View(weapon);
            }

            this.weaponsService.Create(
                    weapon.Name,
                    weapon.IntrinsicName,
                    weapon.IntrinsicDescription,
                    weapon.CatalystName,
                    weapon.CatalystCompletionRequirement,
                    weapon.CatalystEffect,
                    weapon.ClassId,
                    weapon.ImageUrl,
                    this.User.GetId());

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All([FromQuery]AllWeaponsQueryModel query)
        {
            var weapons = this.weaponsService.All(
                        query.SearchTerm, 
                        query.WeaponType, 
                        query.WeaponsPerPage,
                        query.CurrentPage);

            query.Weapons = weapons.Weapons;
            query.WeaponTypes = this.weaponsService.AllWeaponTypes();
            query.AllWeapons = weapons.AllWeapons;

            return View(query);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var weapon = this.weaponsService.GetById(id);

            if (weapon == null)
            {
                return NotFound();
            }
            //TODO: Add DateCreated and DateModified(?) to comments
            var model = new FullWeaponDetailsViewModel
            {
                Weapon = weapon,
                Comments = this.commentsService.GetByWeaponId(id),
            };

            //TODO: Remove unneeded classes in View
            //TODO: Ask for opinions on the Views
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var weapon = this.weaponsService.GetById(id);

            if (weapon == null)
            {
                return NotFound();
            }

            var model = new AddWeaponFormModel
            {
                Name = weapon.Name,
                IntrinsicName = weapon.IntrinsicName,
                IntrinsicDescription = weapon.IntrinsicDescription,
                CatalystName = weapon.CatalystName,
                CatalystCompletionRequirement = weapon.CatalystCompletionRequirement,
                CatalystEffect = weapon.CatalystEffect,
                ImageUrl = weapon.ImageUrl,
                ClassId = weapon.ClassId,
                Classes = this.weaponsService.AllClasses()
            };

            if (!this.weaponsService.WeaponClassExists(weapon.ClassId))
            {
                this.ModelState.AddModelError(nameof(model.ClassId), "Weapon class does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                model.Classes = this.weaponsService.AllClasses();
                return View(weapon);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, AddWeaponFormModel newWeapon)
        {
            var weaponId = this.weaponsService.GetIdById(id);

            if (weaponId == 0)
            {
                return NotFound();
            }

            if (!this.weaponsService.WeaponClassExists(newWeapon.ClassId))
            {
                this.ModelState.AddModelError(nameof(newWeapon.ClassId), "Weapon class does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                newWeapon.Classes = this.weaponsService.AllClasses();
                return View(newWeapon);
            }

            this.weaponsService.Edit(
                weaponId,
                newWeapon.Name,
                newWeapon.IntrinsicName, 
                newWeapon.IntrinsicDescription,
                newWeapon.CatalystName,
                newWeapon.CatalystCompletionRequirement,
                newWeapon.CatalystEffect,
                newWeapon.ClassId,
                newWeapon.ImageUrl);

            return RedirectToAction("Details", "Weapons", new { id = weaponId });
        }
    }
}
