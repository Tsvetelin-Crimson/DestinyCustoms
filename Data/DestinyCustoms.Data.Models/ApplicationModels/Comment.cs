namespace DestinyCustoms.Data.Models.ApplicationModels
{
    using System.ComponentModel.DataAnnotations;

    using DestinyCustoms.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int ExoticId { get; set; }

        public Exotic Exotic { get; set; }

        // TODO: Add Replies
    }
}
