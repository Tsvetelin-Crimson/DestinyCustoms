namespace DestinyCustoms.Data.Models.ApplicationModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DestinyCustoms.Data.Common.Models;

    public class Exotic : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        // Exotic perk explanation
        // TODO: Rename to Intrinsic and migrate
        [Required]
        public string WeaponIntrinsic { get; set; }

        public int Rating { get; set; }

        public string Catalyst { get; set; }

        public string CatalystCompletionRequirement { get; set; }

        public int ItemClassId { get; set; }

        public ItemClass ItemClass { get; set; }

        public string AddedById { get; set; }

        public ApplicationUser AddedBy { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Suggestion> Suggestions { get; set; }
    }
}
