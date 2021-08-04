using System.Collections.Generic;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Models.Comments
{
    public class CommentViewModel
    {
        public IEnumerable<CommentServiceModel> Comments { get; set; }

        public AddReplyFormModel ReplyToBeAdded { get; set; }

        public string ItemId { get; set; }

    }
}
