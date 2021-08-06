using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Models.Weapons
{
    public class AllWeaponsQueryModel
    {
        public int CurrentPage { get; set; } = 1;

        public int WeaponsPerPage { get; init; } = 3;

        public int AllWeapons { get; set; }

        public List<string> WeaponTypes { get; set; }

        public string WeaponType { get; set; }

        [Display(Name = "Search by weapon name:")]
        public string SearchTerm { get; set; }

        public List<WeaponServiceModel> Weapons { get; set; }
    }
}
