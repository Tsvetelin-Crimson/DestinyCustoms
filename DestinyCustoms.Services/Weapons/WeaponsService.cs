using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Services.Weapons
{
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
                .ProjectTo<WeaponServiceModel>(this.mapper)
                .ToList();

            return new WeaponsQueryServiceModel
            {
                Weapons = weapons,
                AllWeapons = weaponsQuery.Count(),
            };
        }

        public IEnumerable<WeaponServiceModel> AllUserOwned(string userId)
            => db.Weapons
            .Where(w => w.UserId == userId)
            .ProjectTo<WeaponServiceModel>(this.mapper)
            .ToList();

        public IEnumerable<WeaponServiceModel> MostRecentlyCreated()
            => db.Weapons
            .OrderByDescending(w => w.DateCreated)
            .ProjectTo<WeaponServiceModel>(this.mapper)
            .Take(6)
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
            .Select(w => new WeaponValidationServiceModel { Id = w.Id, UserId = w.UserId }) //TODO: Add map? remove weapon in weapon id
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
            // TODO: Add default Image URL If null
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
            weapon.ImageURL = imageUrl;
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


        public IEnumerable<string> AllWeaponTypes()
            => db.WeaponClasses.Select(i => i.Name).ToList();

        public IEnumerable<WeaponClassServiceModel> AllClasses()
            => db.WeaponClasses
            .ProjectTo<WeaponClassServiceModel>(this.mapper)
            .ToList();

        public bool WeaponClassExists(int classId)
             => this.db.WeaponClasses.Any(c => c.Id == classId);
    }
}
