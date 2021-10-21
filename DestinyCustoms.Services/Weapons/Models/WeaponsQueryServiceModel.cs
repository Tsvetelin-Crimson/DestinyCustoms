using System.Collections.Generic;
using DestinyCustoms.Services.CommonModels;

namespace DestinyCustoms.Services.Weapons.Models
{
    public class WeaponsQueryServiceModel
    {
        public int AllWeapons { get; set; }

        public List<ItemServiceModel> Weapons { get; set; }
    }
}
