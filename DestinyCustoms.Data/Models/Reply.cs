using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using DestinyCustoms.Data.Models;


namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants.Comment;

    public class Reply
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; }

        //public int Likes { get; set; }

        //public int Dislikes { get; set; }
        public string UserId { get; init; }

        public IdentityUser User { get; init; }

        public int CommentId { get; init; }

        public Comment Comment { get; init; }
    }
}
