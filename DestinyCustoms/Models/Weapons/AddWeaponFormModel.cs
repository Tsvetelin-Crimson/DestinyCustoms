using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Models.Weapons
{
    using static Common.DataConstants.Weapon;

    public class AddWeaponFormModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = "Exotic name must be between {2} and {1} symbols.")]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxIntrinsicNameLength, MinimumLength = MinIntrinsicNameLength, ErrorMessage = "Intrinsic name must be between {2} and {1} symbols.")]
        public string IntrinsicName { get; set; }

        [Required]
        [StringLength(MaxIntrinsicDescriptionLength, MinimumLength = MinIntrinsicDescriptionLength, ErrorMessage = "Intrinsic description must be between {2} and {1} symbols.")]
        public string IntrinsicDescription { get; set; }

        [Required]
        [StringLength(MaxCatalystNameLength, MinimumLength = MinCatalystNameLength, ErrorMessage = "Catalyst name must be between {2} and {1} symbols.")]
        public string CatalystName { get; set; }

        [Required]
        [StringLength(MaxCatalystCompletionLength, MinimumLength = MinCatalystCompletionLength, ErrorMessage = "Catalyst description must be between {2} and {1} symbols.")]
        public string CatalystCompletionRequirement { get; set; }

        [Required]
        [StringLength(MaxCatalystEffectLength, MinimumLength = MinCatalystEffectLength, ErrorMessage = "Catalyst description must be between {2} and {1} symbols.")]
        public string CatalystEffect { get; set; }

        [Url]
        public string ImageUrl { get; set; }


        [Display(Name = "Class")]
        public int ClassId { get; set; }

        public IEnumerable<WeaponClassServiceModel> Classes { get; set; }
    }
}
