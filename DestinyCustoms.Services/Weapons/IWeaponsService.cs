﻿using System.Collections.Generic;
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

        IEnumerable<WeaponServiceModel> MostRecentlyCreated();

        DetailsWeaponServiceModel GetById(int id);

        int GetIdById(int id);


        public WeaponValidationServiceModel GetIdAndUserIdById(int id);

        int Create(
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect,
            int classId,
            string imageUrl,
            string userId);

        int Edit(
            int id,
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect,
            int classId,
            string imageUrl);

        public void Delete(int id);

        IEnumerable<string> AllWeaponTypes();

        bool WeaponClassExists(int classId);

        IEnumerable<WeaponClassServiceModel> AllClasses();
    }
}