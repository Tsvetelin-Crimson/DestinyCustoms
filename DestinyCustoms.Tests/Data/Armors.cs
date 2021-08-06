using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Tests.Data
{
    public static class Armors
    {
        public static IEnumerable<ExoticArmor> TenBlankArmors()
            => Enumerable.Range(0, 10)
            .Select(w => new ExoticArmor());
    }
}
