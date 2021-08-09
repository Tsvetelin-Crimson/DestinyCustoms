using System.Collections.Generic;
using DestinyCustoms.Services.CommonModels;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Services.Weapons
{
    public interface IWeaponsService
    {
        WeaponsQueryServiceModel All(
            string searchTerm,
            string weaponType,
            int weaponsPerPage,
            int currentPage);

        List<WeaponServiceModel> AllUserOwned(string userId);

        List<WeaponServiceModel> MostRecentlyCreated();

        List<AdminMostRecentServiceModel> AdminMostRecentlyModified();

        WeaponDetailsServiceModel GetById(string id);

        string GetIdById(string id);

        public WeaponValidationServiceModel GetIdAndUserIdById(string id);

        string Create(
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect,
            int classId,
            string imageUrl,
            string userId);

        string Edit(
            string id,
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect,
            int classId,
            string imageUrl);

        public void Delete(string id);

        List<string> AllWeaponTypes();

        bool WeaponClassExists(int classId);

        List<WeaponClassServiceModel> AllClasses();
    }
}
