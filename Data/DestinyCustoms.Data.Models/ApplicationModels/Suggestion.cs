namespace DestinyCustoms.Data.Models.ApplicationModels
{
    using System.ComponentModel.DataAnnotations;

    using DestinyCustoms.Data.Common.Models;

    public class Suggestion : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }

        public bool IsApproved { get; set; }

        public int ExoticId { get; set; }

        public Exotic Exotic { get; set; }
    }
}
