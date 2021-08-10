using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Models.Armors
{
    using static Common.DataConstants.Armor;
    public class AddArmorFormModel
    {
        [Required]
        [Display(Name = "Exotic Armor Name")]
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

        [Url]
        [Display(Name = "ImageUrl for the Armor (Optional)")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Character Class")]
        public string Class { get; set; }
    }
}
