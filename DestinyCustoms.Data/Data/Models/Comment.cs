using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCommentContentLength)]
        public string Content { get; set; }

        //public int Likes { get; set; }

        //public int Dislikes { get; set; }

        public int ExoticId { get; set; }

        public ExoticWeapon Exotic { get; set; }

        // TODO: Add Replies
    }
}
