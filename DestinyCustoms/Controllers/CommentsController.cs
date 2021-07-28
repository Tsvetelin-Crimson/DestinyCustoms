using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Services.Weapons;
using DestinyCustoms.Services.Comments;
using DestinyCustoms.Infrastructure;

namespace DestinyCustoms.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IWeaponsService weaponsService;
        private readonly ICommentsService commentsService;

        public CommentsController(IWeaponsService weaponsService, ICommentsService commentsService)
        {
            this.weaponsService = weaponsService;
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(FullWeaponDetailsViewModel fullModel)
        {
            var weaponId = weaponsService.GetIdById(fullModel.CommentToBeAdded.WeaponId);

            if (weaponId == null)
            {
                this.ModelState.AddModelError(nameof(fullModel.CommentToBeAdded.WeaponId), "Weapon does not exist");
            }

            if (!this.ModelState.IsValid) //TODO: make it so it returns the page with an error message
            {
                return BadRequest();
            }

            this.commentsService.Create(
                fullModel.CommentToBeAdded.Content,
                fullModel.CommentToBeAdded.WeaponId,
                this.User.GetId());

            return Redirect($"/Weapons/Details/{fullModel.CommentToBeAdded.WeaponId}");
        }
    }
}
