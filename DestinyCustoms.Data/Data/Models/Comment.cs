using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants.Comment;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; }

        //public int Likes { get; set; }

        //public int Dislikes { get; set; }
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public string WeaponId { get; set; }

        public ExoticWeapon Exotic { get; set; }

        // TODO: Add Replies
    }
}
