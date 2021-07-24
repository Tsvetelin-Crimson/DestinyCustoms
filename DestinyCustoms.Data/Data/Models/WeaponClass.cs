using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Data.Models
{
    using static Common.DataConstants;

    public class WeaponClass
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxWeaponClassNameLength)]
        public string Name { get; set; }

        public ICollection<ExoticWeapon> Weapons { get; set; }
    }
}
