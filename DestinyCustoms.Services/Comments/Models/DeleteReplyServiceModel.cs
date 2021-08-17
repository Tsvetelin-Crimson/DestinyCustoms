namespace DestinyCustoms.Services.Comments.Models
{
    public class DeleteReplyServiceModel
    {
        public int ReplyId { get; set; }

        public int CommentId { get; set; }

        public string ItemId { get; set; }

        public string UserId { get; set; }

        public string AspActionString { get; set; }
    }
}
