using System.Collections.Generic;
using DestinyCustoms.Services.CommonModels;

namespace DestinyCustoms.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<ItemServiceModel> Weapons { get; set; }

        public IEnumerable<ItemServiceModel> Armors { get; set; }
    }
}
