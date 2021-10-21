using System.Collections.Generic;
using DestinyCustoms.Services.CommonModels;

namespace DestinyCustoms.Services.Armors.Models
{
    public class ArmorsQueryServiceModel
    {
        public int AllArmorsCount { get; set; }

        public List<ItemServiceModel> Armors { get; set; }
    }
}
