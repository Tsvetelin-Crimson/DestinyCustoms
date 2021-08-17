using DestinyCustoms.Models.Comments;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Models.Weapons
{
    public class FullWeaponDetailsViewModel
    {
        public WeaponDetailsServiceModel Weapon { get; init; }

        public AddCommentFormModel CommentToBeAdded { get; set; }

        public CommentViewModel CommentClass { get; set; }
    }
}
