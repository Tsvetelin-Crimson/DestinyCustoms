using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int ExoticId { get; set; }

        public Exotic Exotic { get; set; }

        // TODO: Add Replies
    }
}
