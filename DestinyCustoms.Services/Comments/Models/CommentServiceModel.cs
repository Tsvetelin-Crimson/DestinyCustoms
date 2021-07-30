using System.Collections.Generic;

namespace DestinyCustoms.Services.Comments.Models
{
    public class CommentServiceModel
    {
        public int Id { get; init; }

        public string Content { get; init; }

        public string UserUsername { get; init; }

        public IEnumerable<ReplyServiceModel> Replies { get; init; }
    }
}
