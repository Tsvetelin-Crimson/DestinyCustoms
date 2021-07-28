using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants.WeaponClass;

    public class WeaponClass
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public ICollection<ExoticWeapon> Weapons { get; set; }
    }
}
