using System.Collections.Generic;
using DestinyCustoms.Models.Weapons;

namespace DestinyCustoms.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<AllWeaponsViewModel> Weapons { get; set; }
    }
}
