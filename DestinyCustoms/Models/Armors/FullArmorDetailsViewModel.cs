using System.Collections.Generic;
using DestinyCustoms.Models.Comments;
using DestinyCustoms.Services.Armors.Models;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Models.Armors
{
    public class FullArmorDetailsViewModel
    {
        public ArmorDetailsServiceModel Armor { get; set; }

        public AddCommentFormModel CommentToBeAdded { get; set; }

        public IEnumerable<CommentServiceModel> Comments { get; set; }

        public AddReplyFormModel ReplyToBeAdded { get; set; }
    }
}
