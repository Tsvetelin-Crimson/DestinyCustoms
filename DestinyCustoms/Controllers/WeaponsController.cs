using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Comments;
using DestinyCustoms.Models.Comments;

namespace DestinyCustoms.Controllers
{
    using static Common.WebConstants;

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

        public IActionResult All([FromQuery] AllWeaponsQueryModel query)
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

            var weaponId = this.weaponsService.Create(
                    weapon.Name,
                    weapon.IntrinsicName,
                    weapon.IntrinsicDescription,
                    weapon.CatalystName,
                    weapon.CatalystCompletionRequirement,
                    weapon.CatalystEffect,
                    weapon.ClassId,
                    weapon.ImageUrl,
                    this.User.GetId());

            return RedirectToAction(
                nameof(WeaponsController.Details),
                nameof(WeaponsController).RemoveControllerFromString(), 
                new { id = weaponId });
        }

        public IActionResult Details(string id)
        {
            var weapon = this.weaponsService.GetById(id);

            if (weapon == null)
            {
                return NotFound();
            }

            var model = new FullWeaponDetailsViewModel
            {
                Weapon = weapon,
                CommentToBeAdded = new AddCommentFormModel
                {
                    ItemId = weapon.Id,
                    AspActionString = nameof(CommentsController.AddWeaponComment),
                },
                CommentClass = new CommentViewModel
                {
                    Comments = this.commentsService.GetByWeaponId(id),
                    ItemId = weapon.Id,
                    DeleteModel = new DeleteCommentFormModel
                    {
                        ItemId = weapon.Id,
                        AspActionString = nameof(CommentsController.DeleteWeaponComment),
                    },
                    ReplyToBeAdded = new AddReplyFormModel 
                    {
                        ItemId = weapon.Id,
                        AspActionReplyString = nameof(CommentsController.AddWeaponReply),
                    },
                },
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var weapon = this.weaponsService.GetById(id);

            if (weapon == null)
            {
                return NotFound();
            }

            if (this.User.GetId() != weapon.UserId && !this.User.IsInRole(adminRoleName))
            {
                return Unauthorized();
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

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(string id, AddWeaponFormModel newWeapon)
        {
            var weapon = this.weaponsService.GetIdAndUserIdById(id);  

            if (weapon == null)
            {
                return NotFound();
            }

            if (this.User.GetId() != weapon.UserId && !this.User.IsInRole(adminRoleName))
            {
                return Unauthorized();
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
                weapon.Id,
                newWeapon.Name,
                newWeapon.IntrinsicName, 
                newWeapon.IntrinsicDescription,
                newWeapon.CatalystName,
                newWeapon.CatalystCompletionRequirement,
                newWeapon.CatalystEffect,
                newWeapon.ClassId,
                newWeapon.ImageUrl);

            return RedirectToAction(
                nameof(WeaponsController.Details), 
                nameof(WeaponsController).RemoveControllerFromString(), 
                new { id = weapon.Id });
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            var weapon = this.weaponsService.GetIdAndUserIdById(id);

            if (weapon == null)
            {
                return NotFound();
            }

            if (this.User.GetId() != weapon.UserId && !this.User.IsInRole(adminRoleName))
            {
                return Unauthorized();
            }

            this.weaponsService.Delete(weapon.Id);

            return RedirectToAction(nameof(WeaponsController.MyWeapons), nameof(WeaponsController).RemoveControllerFromString());
        }

        [Authorize]
        public IActionResult MyWeapons() 
            => View(this.weaponsService.AllUserOwned(this.User.GetId()));
    }
}
