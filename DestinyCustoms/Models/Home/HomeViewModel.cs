using System.Collections.Generic;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<WeaponServiceModel> Weapons { get; set; }
    }
}
