using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    public class Exotic
    {
        //TODO: Add validations and min-max values
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string WeaponIntrinsic { get; set; }

        public int Rating { get; set; }

        public string Catalyst { get; set; }

        public string CatalystCompletionRequirement { get; set; }

        public int ItemClassId { get; set; }

        public ItemClass ItemClass { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Suggestion> Suggestions { get; set; }
    }
}
