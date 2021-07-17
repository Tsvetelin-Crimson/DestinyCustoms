using Microsoft.AspNetCore.Mvc;
using DestinyCustoms.Data;
using DestinyCustoms.Models.Weapons;
using System.Linq;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Controllers
{
    public class CommentsController : Controller
    {

        private readonly DestinyCustomsDbContext db;

        public CommentsController(DestinyCustomsDbContext db)
            => this.db = db;

        [HttpPost]
        public IActionResult Add(FullWeaponDetailsViewModel fullModel)
        {
            var weaponId = db.Weapons
                .Where(w => w.Id == fullModel.CommentToBeAdded.WeaponId)
                .Select(w => w.Id)
                .FirstOrDefault();

            if (weaponId == null)
            {
                this.ModelState.AddModelError(nameof(fullModel.CommentToBeAdded.WeaponId), "Weapon does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = new Comment()
            {
                Content = fullModel.CommentToBeAdded.Content,
                ExoticId = fullModel.CommentToBeAdded.WeaponId,
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            return Redirect($"/Weapons/Details/{fullModel.CommentToBeAdded.WeaponId}");
        }
    }
}
