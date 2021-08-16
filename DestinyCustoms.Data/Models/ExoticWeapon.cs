using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants.Weapon;

    public class ExoticWeapon
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxIntrinsicNameLength)]
        public string IntrinsicName { get; set; }

        [Required]
        [MaxLength(MaxIntrinsicDescriptionLength)]
        public string IntrinsicDescription { get; set; }

        [Required]
        [MaxLength(MaxCatalystNameLength)]
        public string CatalystName { get; set; }

        [Required]
        [MaxLength(MaxCatalystCompletionLength)]
        public string CatalystCompletionRequirement { get; set; }

        [Required]
        [MaxLength(MaxCatalystEffectLength)]
        public string CatalystEffect { get; set; }

        public string ImageURL { get; set; }

        public DateTime DateCreated { get; init; }

        public DateTime DateModified { get; set; }

        [Required]
        public string UserId { get; init; }

        public IdentityUser User { get; set; }

        public int WeaponClassId { get; set; }

        public WeaponClass WeaponClass { get; set; }

        public ICollection<Comment> Comments { get; init; }
    }
}
