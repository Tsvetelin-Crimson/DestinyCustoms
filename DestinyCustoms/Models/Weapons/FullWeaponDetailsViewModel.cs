using System.Collections.Generic;
using DestinyCustoms.Models.Comments;
using DestinyCustoms.Services.Weapons.Models;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Models.Weapons
{
    public class FullWeaponDetailsViewModel
    {
        public WeaponDetailsServiceModel Weapon { get; init; }

        public AddCommentFormModel CommentToBeAdded { get; set; }

        public CommentViewModel CommentClass { get; set; }

    }
}
