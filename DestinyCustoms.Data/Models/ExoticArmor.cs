using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using DestinyCustoms.Common.Enums;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants.Armor;

    public class ExoticArmor
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
        public DateTime DateCreated { get; init; }

        public DateTime DateModified { get; set; }

        public string ImageURL { get; set; }

        [Required]
        public string UserId { get; init; }

        public IdentityUser User { get; set; }

        [Required]
        public CharacterClass CharacterClass { get; set; }

    }
}
