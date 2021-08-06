using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Comments;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Models.Armors;
using DestinyCustoms.Services.Armors;
using DestinyCustoms.Models.Comments;

namespace DestinyCustoms.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IWeaponsService weaponsService;
        private readonly IArmorsService armorsService;
        private readonly ICommentsService commentsService;

        public CommentsController(
            IWeaponsService weaponsService, 
            IArmorsService armorsService, 
            ICommentsService commentsService)
        {
            this.weaponsService = weaponsService;
            this.armorsService = armorsService;
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddWeaponComment(AddCommentFormModel comment)
        {
            var weaponId = this.weaponsService.GetIdById(comment.ItemId);

            if (weaponId == null)
            {
                this.ModelState.AddModelError(nameof(comment.ItemId), "Weapon does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = comment.ItemId });
            }

            this.commentsService.CreateWeaponComment(
                comment.Content,
                comment.ItemId,
                this.User.GetId());

            return RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = comment.ItemId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddWeaponReply(AddReplyFormModel reply)
        {
            var weaponId = this.weaponsService.GetIdById(reply.ItemId);

            if (weaponId == null)
            {
                return BadRequest();
            }

            var commentId = this.commentsService.GetById(reply.CommentId);

            if (commentId == null)
            {
                return BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = reply.ItemId }); ;
            }

            this.commentsService.CreateReply(
                reply.Content,
                reply.CommentId,
                this.User.GetId());

            return RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = reply.ItemId }); ;

        }

        [HttpPost]
        [Authorize]
        public IActionResult AddArmorComment(AddCommentFormModel comment)
        {
            var armorId = armorsService.GetIdById(comment.ItemId);

            if (armorId == null)
            {
                this.ModelState.AddModelError(nameof(comment.ItemId), "Armor does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(
                    nameof(ArmorsController.Details),
                    nameof(ArmorsController).RemoveControllerFromString(),
                    new { id = comment.ItemId });
            }

            this.commentsService.CreateArmorComment(
                comment.Content,
                comment.ItemId,
                this.User.GetId());

            return RedirectToAction(
                    nameof(ArmorsController.Details),
                    nameof(ArmorsController).RemoveControllerFromString(),
                    new { id = comment.ItemId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddArmorReply(AddReplyFormModel reply)
        {
            var armorId = armorsService.GetIdById(reply.ItemId);

            if (armorId == null)
            {
                return BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(
                    nameof(ArmorsController.Details),
                    nameof(ArmorsController).RemoveControllerFromString(),
                    new { id = reply.ItemId });
            }

            this.commentsService.CreateReply(
                reply.Content,
                reply.CommentId,
                this.User.GetId());

            return RedirectToAction(
                    nameof(ArmorsController.Details),
                    nameof(ArmorsController).RemoveControllerFromString(),
                    new { id = reply.ItemId });
        }
    }
}
