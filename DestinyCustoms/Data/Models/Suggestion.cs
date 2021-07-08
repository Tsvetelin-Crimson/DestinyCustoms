using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    public class Suggestion
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsApproved { get; set; }

        public int ExoticId { get; set; }

        public Exotic Exotic { get; set; }
    }
}
