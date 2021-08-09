using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DestinyCustoms.Services.Armors.Models;

namespace DestinyCustoms.Models.Armors
{
    public class AllArmorsQueryModel
    {
        public int CurrentPage { get; set; } = 1;

        public int ArmorsPerPage { get; init; } = 3;

        public int AllArmorsCount { get; set; }

        public List<string> Classes { get; set; }

        public string Class { get; set; }

        [Display(Name = "Search by weapon name:")]
        public string SearchTerm { get; set; }

        public List<ArmorServiceModel> Armors { get; set; }
    }
}
