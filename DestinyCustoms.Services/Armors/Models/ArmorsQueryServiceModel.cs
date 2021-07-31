using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCustoms.Services.Armors.Models
{
    public class ArmorsQueryServiceModel
    {
        public int AllArmorsCount { get; set; }

        public IEnumerable<ArmorServiceModel> Armors { get; set; }
    }
}
