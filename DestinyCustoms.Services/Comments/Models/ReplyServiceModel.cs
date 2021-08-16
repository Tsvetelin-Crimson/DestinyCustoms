namespace DestinyCustoms.Services.Comments.Models
{
    public class ReplyServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserUsername { get; set; }

        public string UserId { get; set; }

        public string CreatedOn { get; set; }
    }
}
