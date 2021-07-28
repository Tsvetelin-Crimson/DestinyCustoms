using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Models.Comments
{
    using static DestinyCustoms.Common.DataConstants;

    public class AddCommentFormModel
    {
        [Required]
        [StringLength(MaxCommentContentLength, MinimumLength = MinCommentContentLength, ErrorMessage = "Comment must be between {2} and {1} symbols.")]
        public string Content { get; set; }

        [Required]
        public string WeaponId { get; set; }
    }
}
