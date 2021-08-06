using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data.Models;
using MyTested.AspNetCore.Mvc;

namespace DestinyCustoms.Tests.Data
{
    public static class Weapons
    {
        public static IEnumerable<ExoticWeapon> TenBlankWeaponsWithWeaponClass()
            => Enumerable.Range(0, 10)
            .Select(w => new ExoticWeapon() 
            { 
                WeaponClass = new WeaponClass()
            });

        public static IEnumerable<WeaponClass> TenBlankWeaponClasses()
            => Enumerable.Range(0, 10)
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
