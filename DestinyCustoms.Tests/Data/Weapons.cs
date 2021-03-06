using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MyTested.AspNetCore.Mvc;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Tests.Data
{
    public static class Weapons
    {
        public static IEnumerable<ExoticWeapon> TenBlankWeapons()
            => Enumerable.Range(0, 10)
            .Select(w => new ExoticWeapon() 
            { 
                WeaponClass = new WeaponClass()
            });

        public static IEnumerable<ExoticWeapon> ThirtyBlankWeapons()
            => Enumerable.Range(0, 30)
            .Select(w => new ExoticWeapon()
            {
                WeaponClass = new WeaponClass(),
                User = new IdentityUser(),
                DateModified = DateTime.UtcNow,
            });

        public static IEnumerable<WeaponClass> TenBlankWeaponClasses()
            => Enumerable.Range(0, 10)
            .Select(w => new WeaponClass());

        public static ExoticWeapon OneWeaponWithSetId(string id, string name = null)
            => new()
            {
                Id = id,
                Name = name,
                WeaponClass = new WeaponClass(),
                UserId = TestUser.Identifier,
                User = new IdentityUser
                {
                    Id = TestUser.Identifier
                },
            };

        public static IEnumerable<ExoticWeapon> ThreeWeaponsOneUserOwned(string id, string userId)
        {
            var userOwnedWeapon = new ExoticWeapon
            {
                Id = id,
                WeaponClass = new WeaponClass(),
                UserId = userId,
            };

            var allWeapons = Enumerable.Range(0, 2).Select(w => new ExoticWeapon { WeaponClass = new WeaponClass() }).ToList();

            allWeapons.Add(userOwnedWeapon);

            return allWeapons;
        }
    }
}
