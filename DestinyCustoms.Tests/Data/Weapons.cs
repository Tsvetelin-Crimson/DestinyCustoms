using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data.Models;
using MyTested.AspNetCore.Mvc;

namespace DestinyCustoms.Tests.Data
{
    public static class Weapons
    {
        public static IEnumerable<ExoticWeapon> FiveBlankWeaponsWithWeaponClass()
            => Enumerable.Range(0, 5)
            .Select(w => new ExoticWeapon() 
            { 
                WeaponClass = new WeaponClass()
            });

        public static IEnumerable<WeaponClass> FiveBlankWeaponClasses()
            => Enumerable.Range(0, 5)
            .Select(w => new WeaponClass());

        public static ExoticWeapon OneWeaponsWithWeaponClassAndSetId(string id, string name = null)
            => new()
            {
                Id = id,
                Name = name,
                WeaponClass = new WeaponClass(),
                UserId = TestUser.Identifier
            };
    }
}
