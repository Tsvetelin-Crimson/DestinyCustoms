using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Tests.Data
{
    public static class Armors
    {
        public static IEnumerable<ExoticArmor> FiveBlankArmors()
            => Enumerable.Range(0, 5)
            .Select(w => new ExoticArmor());
    }
}
