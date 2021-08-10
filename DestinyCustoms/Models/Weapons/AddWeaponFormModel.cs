using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Models.Weapons
{
    using static Common.DataConstants.Weapon;

    public class AddWeaponFormModel
    {
        [Required]
        [Display(Name = "Exotic Weapon Name")]
        [StringLength(
            MaxNameLength, 
            MinimumLength = MinNameLength, 
            ErrorMessage = "Exotic name must be between {2} and {1} symbols.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Intrinsic Name")]
        [StringLength(
            MaxIntrinsicNameLength, 
            MinimumLength = MinIntrinsicNameLength, 
            ErrorMessage = "Intrinsic name must be between {2} and {1} symbols.")]
        public string IntrinsicName { get; set; }

        [Required]
        [Display(Name = "Describe what the intrinsic will do")]
        [StringLength(
            MaxIntrinsicDescriptionLength, 
            MinimumLength = MinIntrinsicDescriptionLength, 
            ErrorMessage = "Intrinsic description must be between {2} and {1} symbols.")]
        public string IntrinsicDescription { get; set; }

        [Required]
        [Display(Name = "Catalyst Name")]
        [StringLength(
            MaxCatalystNameLength, 
            MinimumLength = MinCatalystNameLength, 
            ErrorMessage = "Catalyst name must be between {2} and {1} symbols.")]
        public string CatalystName { get; set; }

        [Required]
        [Display(Name = "Describe how the catalyst should be completed")]
        [StringLength(
            MaxCatalystCompletionLength, 
            MinimumLength = MinCatalystCompletionLength, 
            ErrorMessage = "Catalyst description must be between {2} and {1} symbols.")]
        public string CatalystCompletionRequirement { get; set; }

        [Required]
        [Display(Name = "Describe what the catalyst does")]
        [StringLength(
            MaxCatalystEffectLength, 
            MinimumLength = MinCatalystEffectLength, 
            ErrorMessage = "Catalyst description must be between {2} and {1} symbols.")]
        public string CatalystEffect { get; set; }

        [Url]
        [Display(Name = "ImageUrl for the weapon (Optional)")]
        public string ImageUrl { get; set; }


        [Display(Name = "Weapon Class")]
        public int ClassId { get; set; }

        public ICollection<WeaponClassServiceModel> Classes { get; set; }
    }
}
