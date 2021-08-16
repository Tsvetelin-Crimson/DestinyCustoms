using System.Collections.Generic;

namespace DestinyCustoms.Services.Comments.Models
{
    public class CommentServiceModel
    {
        public int Id { get; init; }

        public string Content { get; init; }

        public string UserUsername { get; init; }

        public string UserId { get; init; }

        public string CreatedOn { get; set; }

        public IEnumerable<ReplyServiceModel> Replies { get; init; }
    }
}
