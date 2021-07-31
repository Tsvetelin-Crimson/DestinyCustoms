using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants.Comment;

    public class Comment
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; }

        //public int Likes { get; set; }

        //public int Dislikes { get; set; }
        public string UserId { get; init; }

        public IdentityUser User { get; init; }

        public string WeaponId { get; init; }

        public ExoticWeapon Exotic { get; init; }

        public string ArmorId { get; init; }

        public ExoticArmor Armor { get; init; }


        public ICollection<Reply> Replies { get; set; }
    }
}
