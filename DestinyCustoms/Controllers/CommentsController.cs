using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Comments;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Services.Armors;
using DestinyCustoms.Models.Comments;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Controllers
{
    [Authorize]
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

        [HttpPost]
        public IActionResult DeleteWeaponComment(DeleteCommentFormModel comment)
        {
            var weaponId = this.weaponsService.GetIdById(comment.ItemId);

            if (weaponId == null)
            {
                return BadRequest();
            }
            var currComment = this.commentsService.GetById(comment.CommentId);

            if (currComment == null)
            {
                return BadRequest();
            }

            if (this.User.GetId() != currComment.UserId)
            {
                return Unauthorized();
            }

            this.commentsService.DeleteComment(comment.CommentId);

            return RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = comment.ItemId });
        }

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
        public IActionResult DeleteWeaponReply(DeleteReplyServiceModel reply)
        {
            var weaponId = this.weaponsService.GetIdById(reply.ItemId);

            if (weaponId == null)
            {
                return BadRequest();
            }
            var comment = this.commentsService.GetById(reply.CommentId);

            if (comment == null)
            {
                return BadRequest();
            }
            var currReply = this.commentsService.GetReplyById(reply.ReplyId);

            if (currReply == null)
            {
                return BadRequest();
            }
            if (this.User.GetId() != currReply.UserId)
            {
                return Unauthorized();
            }

            this.commentsService.DeleteReply(reply.ReplyId);

            return RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = reply.ItemId });
        }

        [HttpPost]
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

        [HttpPost]
        public IActionResult DeleteArmorComment(DeleteCommentFormModel comment)
        {
            var weaponId = this.armorsService.GetIdById(comment.ItemId);

            if (weaponId == null)
            {
                return BadRequest();
            }
            var currComment = this.commentsService.GetById(comment.CommentId);

            if (currComment == null)
            {
                return BadRequest();
            }

            if (this.User.GetId() != currComment.UserId)
            {
                return Unauthorized();
            }

            this.commentsService.DeleteComment(comment.CommentId);

            return RedirectToAction(
                    nameof(ArmorsController.Details),
                    nameof(ArmorsController).RemoveControllerFromString(),
                    new { id = comment.ItemId });
        }

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

        [HttpPost]
        public IActionResult DeleteArmorReply(DeleteReplyServiceModel reply)
        {
            var armorId = this.armorsService.GetIdById(reply.ItemId);

            if (armorId == null)
            {
                return BadRequest();
            }
            var comment = this.commentsService.GetById(reply.CommentId);

            if (comment == null)
            {
                return BadRequest();
            }
            var currReply = this.commentsService.GetReplyById(reply.ReplyId);

            if (currReply == null)
            {
                return BadRequest();
            }
            if (this.User.GetId() != currReply.UserId)
            {
                return Unauthorized();
            }

            this.commentsService.DeleteReply(reply.ReplyId);

            return RedirectToAction(
                    nameof(ArmorsController.Details),
                    nameof(ArmorsController).RemoveControllerFromString(),
                    new { id = reply.ItemId });
        }
    }
}
