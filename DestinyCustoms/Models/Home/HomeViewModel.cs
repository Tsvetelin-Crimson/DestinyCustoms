using System.Collections.Generic;
using DestinyCustoms.Services.Armors.Models;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<WeaponServiceModel> Weapons { get; set; }

        public IEnumerable<ArmorServiceModel> Armors { get; set; }
    }
}
