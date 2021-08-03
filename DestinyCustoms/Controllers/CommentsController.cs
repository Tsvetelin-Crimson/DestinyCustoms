using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Comments;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Models.Armors;
using DestinyCustoms.Services.Armors;

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
        public IActionResult AddWeaponComment(FullWeaponDetailsViewModel fullModel)
        {
            var weaponId = weaponsService.GetIdById(fullModel.CommentToBeAdded.ItemId);

            if (weaponId == null)
            {
                this.ModelState.AddModelError(nameof(fullModel.CommentToBeAdded.ItemId), "Weapon does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return Redirect($"/Weapons/Details/{fullModel.CommentToBeAdded.ItemId}");
            }

            this.commentsService.CreateWeaponComment(
                fullModel.CommentToBeAdded.Content,
                fullModel.CommentToBeAdded.ItemId,
                this.User.GetId());

            return Redirect($"/Weapons/Details/{fullModel.CommentToBeAdded.ItemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddWeaponReply(FullWeaponDetailsViewModel fullModel)
        {
            var weaponId = weaponsService.GetIdById(fullModel.ReplyToBeAdded.ItemId);

            if (weaponId == null)
            {
                return BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return Redirect($"/Weapons/Details/{fullModel.ReplyToBeAdded.ItemId}");
            }

            this.commentsService.CreateReply(
                fullModel.ReplyToBeAdded.Content, 
                fullModel.ReplyToBeAdded.CommentId, 
                this.User.GetId());

            return Redirect($"/Weapons/Details/{fullModel.ReplyToBeAdded.ItemId}");
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddArmorComment(FullArmorDetailsViewModel fullModel)
        {
            var armorId = armorsService.GetIdById(fullModel.CommentToBeAdded.ItemId);

            if (armorId == null)
            {
                this.ModelState.AddModelError(nameof(fullModel.CommentToBeAdded.ItemId), "Armor does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return Redirect($"/Armors/Details/{fullModel.CommentToBeAdded.ItemId}");
            }

            this.commentsService.CreateArmorComment(
                fullModel.CommentToBeAdded.Content,
                fullModel.CommentToBeAdded.ItemId,
                this.User.GetId());

            return Redirect($"/Armors/Details/{fullModel.CommentToBeAdded.ItemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddArmorReply(FullArmorDetailsViewModel fullModel)
        {
            var armorId = armorsService.GetIdById(fullModel.ReplyToBeAdded.ItemId);

            if (armorId == null)
            {
                return BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return Redirect($"/Armors/Details/{fullModel.ReplyToBeAdded.ItemId}");
            }

            this.commentsService.CreateReply(
                fullModel.ReplyToBeAdded.Content,
                fullModel.ReplyToBeAdded.CommentId,
                this.User.GetId());

            return Redirect($"/Armors/Details/{fullModel.ReplyToBeAdded.ItemId}");
        }
    }
}
