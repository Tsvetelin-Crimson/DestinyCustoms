using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data.Models;
using MyTested.AspNetCore.Mvc;

namespace DestinyCustoms.Tests.Data
{
    public static class Armors
    {
        public static IEnumerable<ExoticArmor> TenBlankArmors()
            => Enumerable.Range(0, 10)
            .Select(w => new ExoticArmor());

        public static ExoticArmor OneArmorWithSetId(string id, string name = null)
            => new()
            {
                Id = id,
                Name = name,
                UserId = TestUser.Identifier,
            };

        public static IEnumerable<ExoticArmor> ThreeArmorsOneUserOwned(string id, string userId)
        {
            var userOwnedWeapon = new ExoticArmor
            {
                Id = id,
                UserId = userId,
            };

            var allArmors = Enumerable.Range(0, 2).Select(w => new ExoticArmor()).ToList();

            allArmors.Add(userOwnedWeapon);

            return allArmors;
        }
    }
}
