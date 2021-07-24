using DestinyCustoms.Services.Weapons.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DestinyCustoms.Models.Weapons
{
    public class AllWeaponsQueryModel
    {
        public int CurrentPage { get; set; } = 1;

        public int WeaponsPerPage { get; init; } = 3;

        public int AllWeapons { get; set; }

        public IEnumerable<string> WeaponTypes { get; set; }

        public string WeaponType { get; set; }

        [Display(Name = "Search by weapon name:")]
        public string SearchTerm { get; set; }

        public IEnumerable<WeaponServiceModel> Weapons { get; set; }
    }
}
