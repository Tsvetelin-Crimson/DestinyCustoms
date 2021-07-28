using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Models.Comments
{
    using static DestinyCustoms.Common.DataConstants.Comment;

    public class AddCommentFormModel
    {
        [Required]
        [StringLength(MaxContentLength, MinimumLength = MinContentLength, ErrorMessage = "Comment must be between {2} and {1} symbols.")]
        public string Content { get; set; }

        [Required]
        public string WeaponId { get; set; }
    }
}
