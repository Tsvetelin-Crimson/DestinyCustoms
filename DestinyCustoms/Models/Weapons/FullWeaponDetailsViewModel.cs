using System.Collections.Generic;
using DestinyCustoms.Models.Comments;
using DestinyCustoms.Services.Weapons.Models;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Models.Weapons
{
    public class FullWeaponDetailsViewModel
    {
        public DetailsWeaponServiceModel Weapon { get; init; }

        public AddCommentFormModel CommentToBeAdded { get; set; }

        public IEnumerable<CommentServiceModel> Comments { get; set; }

    }
}
