using System.Collections.Generic;

namespace DestinyCustoms.Services.Weapons.Models
{
    public class WeaponsQueryServiceModel
    {
        public int AllWeapons { get; set; }

        public IEnumerable<WeaponServiceModel> Weapons { get; set; }
    }
}
