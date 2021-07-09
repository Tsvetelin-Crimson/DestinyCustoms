using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants;

    public class Suggestion
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxSuggestionContentLength)]
        public string Content { get; set; }
        
        //TODO: Need users
        //public bool IsApproved { get; set; }

        public int ExoticId { get; set; }

        public ExoticWeapon Exotic { get; set; }
    }
}
