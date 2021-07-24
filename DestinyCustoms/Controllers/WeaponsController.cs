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
    }
}
