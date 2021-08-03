using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Models.Comments
{
    using static DestinyCustoms.Common.DataConstants.Comment;

    public class AddReplyFormModel
    {
        [Required]
        [StringLength(
            MaxContentLength, 
            MinimumLength = MinContentLength, 
            ErrorMessage = "Reply must be between {2} and {1} symbols.")]
        public string Content { get; set; }

        [Required]
        public int CommentId { get; set; }

        [Required]
        public string ItemId { get; set; }
    }
}
