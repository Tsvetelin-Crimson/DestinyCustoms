using System.Collections.Generic;

namespace DestinyCustoms.Services.Armors.Models
{
    public class ArmorsQueryServiceModel
    {
        public int AllArmorsCount { get; set; }

        public List<ArmorServiceModel> Armors { get; set; }
    }
}
