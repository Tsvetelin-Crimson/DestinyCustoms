using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Models.Comments
{
    public class DeleteCommentFormModel
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        public string ItemId { get; set; }

        public string AspActionString { get; set; }
    }
}
