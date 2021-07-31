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

        ArmorDetailsServiceModel GetById(string id);

        string GetIdById(string id);


        ArmorValidationServiceModel GetIdAndUserIdById(string id);

        IEnumerable<ArmorServiceModel> MostRecentlyCreated();

        string Create(
            string name,
            string intrinsicName,
            string intrinsicDescription,
            CharacterClass classEnum,
            string imageUrl,
            string userId);

        string Edit(
            string id,
            string name,
            string intrinsicName,
            string intrinsicDescription,
            CharacterClass classEnum,
            string imageUrl);

        void Delete(string id);

        IEnumerable<string> AllClassNames();

        (bool, CharacterClass) IsCharacterClassNameValid(string name);

    }
}
