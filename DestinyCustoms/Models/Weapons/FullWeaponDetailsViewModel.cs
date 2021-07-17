using System.Collections.Generic;
using DestinyCustoms.Models.Comments;

namespace DestinyCustoms.Models.Weapons
{
    public class FullWeaponDetailsViewModel
    {
        public DetailsWeaponViewModel Weapon { get; init; }

        public AddCommentFormModel CommentToBeAdded { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

    }
}
