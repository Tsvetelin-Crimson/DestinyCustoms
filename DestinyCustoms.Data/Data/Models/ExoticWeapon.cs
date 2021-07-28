using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants;

    public class ExoticWeapon
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MaxWeaponNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxWeaponIntrinsicNameLength)]
        public string IntrinsicName { get; set; }

        [Required]
        [MaxLength(MaxWeaponIntrinsicDescriptionLength)]
        public string IntrinsicDescription { get; set; }

        public string ImageURL { get; set; }

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

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        public string UserId { get; set; }

        public int WeaponClassId { get; set; }

        public WeaponClass WeaponClass { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
