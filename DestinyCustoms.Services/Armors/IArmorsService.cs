using System.Collections.Generic;
using DestinyCustoms.Common.Enums;
using DestinyCustoms.Services.Armors.Models;

namespace DestinyCustoms.Services.Armors
{
    public interface IArmorsService
    {
        ArmorsQueryServiceModel All(
            string searchTerm,
            string className,
            int armorsPerPage,
            int currentPage);

        public IEnumerable<ArmorServiceModel> MostRecentlyCreated();

        string Create(
            string name,
            string intrinsicName,
            string intrinsicDescription,
            CharacterClass classEnum,
            string imageUrl,
            string userId);

        IEnumerable<string> AllClassNames();
    }
}
