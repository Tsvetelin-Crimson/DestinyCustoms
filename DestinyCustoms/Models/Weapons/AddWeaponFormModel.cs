using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Models.Weapons
{
    using static Common.DataConstants;

    public class AddWeaponFormModel
    {
        [Required]
        [StringLength(MaxWeaponNameLength, MinimumLength = MinWeaponNameLength, ErrorMessage = "Exotic name must be between {2} and {1} symbols.")]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxWeaponIntrinsicNameLength, MinimumLength = MinWeaponIntrinsicNameLength, ErrorMessage = "Intrinsic name must be between {2} and {1} symbols.")]
        public string IntrinsicName { get; set; }

        [Required]
        [StringLength(MaxWeaponIntrinsicDescriptionLength, MinimumLength = MinWeaponIntrinsicDescriptionLength, ErrorMessage = "Intrinsic description must be between {2} and {1} symbols.")]
        public string IntrinsicDescription { get; set; }

        [Required]
        [StringLength(MaxWeaponCatalystNameLength, MinimumLength = MinWeaponCatalystNameLength, ErrorMessage = "Catalyst name must be between {2} and {1} symbols.")]
        public string CatalystName { get; set; }

        [Required]
        [StringLength(MaxWeaponCatalystCompletionLength, MinimumLength = MinWeaponCatalystCompletionLength, ErrorMessage = "Catalyst description must be between {2} and {1} symbols.")]
        public string CatalystCompletionRequirement { get; set; }

        [Required]
        [StringLength(MaxWeaponCatalystEffectLength, MinimumLength = MinWeaponCatalystEffectLength, ErrorMessage = "Catalyst description must be between {2} and {1} symbols.")]
        public string CatalystEffect { get; set; }

        [Display(Name = "Class")]
        public int ClassId { get; set; }

        public IEnumerable<WeaponClassViewModel> Classes { get; set; }
    }
}
