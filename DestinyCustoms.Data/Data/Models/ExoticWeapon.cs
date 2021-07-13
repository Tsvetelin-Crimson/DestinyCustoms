using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants;

    public class ExoticWeapon
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxWeaponNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxWeaponIntrinsicNameLength)]
        public string WeaponIntrinsicName { get; set; }

        [Required]
        [MaxLength(MaxWeaponIntrinsicDescriptionLength)]
        public string WeaponIntrinsicDescription { get; set; }

        //TODO: Implement a rating system when you understand Identity and users
        //public int Rating { get; set; }
        [Required]
        [MaxLength(MaxWeaponCatalystNameLength)]
        public string CatalystName { get; set; }

        [Required]
        [MaxLength(MaxWeaponCatalystCompletionLength)]
        public string CatalystCompletionRequirement { get; set; }

        [Required]
        [MaxLength(MaxWeaponCatalystEffectLength)]
        public string CatalystEffect { get; set; }

        public int WeaponClassId { get; set; }

        public WeaponClass WeaponClass { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Suggestion> Suggestions { get; set; }
    }
}
