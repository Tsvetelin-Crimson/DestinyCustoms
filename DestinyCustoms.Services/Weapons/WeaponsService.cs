using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Weapons.Models;
using DestinyCustoms.Services.CommonModels;

namespace DestinyCustoms.Services.Weapons
{

    using static Common.DataConstants.Weapon;
    using static Common.WebConstants;
    public class WeaponsService : IWeaponsService
    {
        private readonly DestinyCustomsDbContext db;
        private readonly IConfigurationProvider mapper;

        public WeaponsService(DestinyCustomsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper.ConfigurationProvider;
        } 

        public WeaponsQueryServiceModel All(
            string searchTerm,
            string weaponType,
            int weaponsPerPage,
            int currentPage)
        {
            var weaponsQuery = db.Weapons.AsQueryable();
 
            if (!string.IsNullOrEmpty(searchTerm))
            {
                weaponsQuery = weaponsQuery.Where(w => w.Name.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(weaponType) && weaponType != "All")
            {
                weaponsQuery = weaponsQuery.Where(w => w.WeaponClass.Name == weaponType);
            }

            var weapons = weaponsQuery
                .OrderByDescending(w => w.Id)
                .Skip(weaponsPerPage * (currentPage - 1))
                .Take(weaponsPerPage)
                .ProjectTo<ItemServiceModel>(this.mapper)
                .ToList();

            return new WeaponsQueryServiceModel
            {
                Weapons = weapons.ToList(),
                AllWeapons = weaponsQuery.Count(),
            };
        }

        public List<ItemServiceModel> AllUserOwned(string userId)
            => db.Weapons
            .Where(w => w.UserId == userId)
            .ProjectTo<ItemServiceModel>(this.mapper)
            .ToList();

        public List<ItemServiceModel> MostRecentlyCreated()
            => db.Weapons
            .OrderByDescending(w => w.DateCreated)
            .ProjectTo<ItemServiceModel>(this.mapper)
            .Take(HomePageNumberOfItems)
            .ToList();

        public List<AdminMostRecentServiceModel> AdminMostRecentlyModified() 
            => db.Weapons
            .OrderByDescending(w => w.DateModified)
            .ProjectTo<AdminMostRecentServiceModel>(this.mapper)
            .Take(AdminHomePageNumberOfItems)
            .ToList();

        public WeaponDetailsServiceModel GetById(string id)
            => db.Weapons
            .Where(w => w.Id == id)
            .ProjectTo<WeaponDetailsServiceModel>(this.mapper)
            .FirstOrDefault();

        public string GetIdById(string id)
            => db.Weapons
            .Where(w => w.Id == id)
            .Select(w => w.Id)
            .FirstOrDefault();

        public WeaponValidationServiceModel GetIdAndUserIdById(string id)
            => db.Weapons
            .Where(w => w.Id == id)
            .ProjectTo<WeaponValidationServiceModel>(this.mapper)
            .FirstOrDefault();

        public string Create(
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect,
            int classId,
            string imageUrl,
            string userId)
        {
            if (imageUrl == null)
            {
                imageUrl = DefaultImageUrl;
            }

            var weaponData = new ExoticWeapon
            {
                Name = name,
                IntrinsicName = intrinsicName,
                IntrinsicDescription = intrinsicDescription,
                CatalystName = catalystName,
                CatalystCompletionRequirement = catalystCompletionRequirement,
                CatalystEffect = catalystEffect,
                WeaponClassId = classId,
                ImageURL = imageUrl,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                UserId = userId,
            };

            this.db.Weapons.Add(weaponData);
            this.db.SaveChanges();

            return weaponData.Id;
        }


        public string Edit(
            string id,
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect, int classId,
            string imageUrl)
        {
            var weapon = db.Weapons.Find(id);

            weapon.Name = name;
            weapon.IntrinsicName = intrinsicName;
            weapon.IntrinsicDescription = intrinsicDescription;
            weapon.CatalystName = catalystName;
            weapon.CatalystCompletionRequirement = catalystCompletionRequirement;
            weapon.CatalystEffect = catalystEffect;
            weapon.WeaponClassId = classId;
            weapon.ImageURL = imageUrl ?? DefaultImageUrl;
            weapon.DateModified = DateTime.UtcNow;

            db.SaveChanges();

            return id;
        }

        public void Delete(string id)
        {
            var weapon = db.Weapons
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (weapon != null)
            {
                db.Weapons.Remove(weapon);
                db.SaveChanges();
            }
        }


        public List<string> AllWeaponTypes()
            => db.WeaponClasses.Select(i => i.Name).ToList();

        public List<WeaponClassServiceModel> AllClasses()
            => db.WeaponClasses
            .ProjectTo<WeaponClassServiceModel>(this.mapper)
            .ToList();

        public bool WeaponClassExists(int classId)
             => this.db.WeaponClasses.Any(c => c.Id == classId);
    }
}
