using System;
using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Services.Weapons
{
    public class WeaponsService : IWeaponsService
    {
        private readonly DestinyCustomsDbContext db;

        public WeaponsService(DestinyCustomsDbContext db) 
            => this.db = db;

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
                .Select(w => new WeaponServiceModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    ClassName = w.WeaponClass.Name,
                    ImageUrl = w.ImageURL,
                })
                .ToList();

            return new WeaponsQueryServiceModel
            {
                Weapons = weapons,
                AllWeapons = weaponsQuery.Count(),
            };
        }

        public IEnumerable<WeaponServiceModel> MostRecentlyCreated()
            => db.Weapons
            .OrderByDescending(w => w.DateCreated)
            .Select(w => new WeaponServiceModel
            {
                Id = w.Id,
                Name = w.Name,
                ClassName = w.WeaponClass.Name,
                ImageUrl = w.ImageURL,
            })
            .Take(4)
            .ToList();

        public int Create(
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
                WeaponIntrinsicName = intrinsicName,
                WeaponIntrinsicDescription = intrinsicDescription,
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

        public DetailsWeaponServiceModel GetById(int id)
                => db.Weapons
                    .Where(w => w.Id == id)
                    .Select(w => new DetailsWeaponServiceModel
                    {
                        Id = w.Id,
                        Name = w.Name,
                        IntrinsicName = w.WeaponIntrinsicName,
                        IntrinsicDescription = w.WeaponIntrinsicDescription,
                        CatalystName = w.CatalystName,
                        CatalystCompletionRequirement = w.CatalystCompletionRequirement,
                        CatalystEffect = w.CatalystEffect,
                        ClassName = w.WeaponClass.Name,
                        ImageUrl = w.ImageURL,
                    })
                    .FirstOrDefault();

        public int GetIdById(int id)
            => db.Weapons
                .Where(w => w.Id == id)
                .Select(w => w.Id)
                .FirstOrDefault();

        public IEnumerable<string> AllWeaponTypes()
            => db.WeaponClasses.Select(i => i.Name).ToList();

        public IEnumerable<WeaponClassServiceModel> AllClasses()
            => db.WeaponClasses
                .Select(x => new WeaponClassServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

        public bool WeaponClassExists(int classId)
             => this.db.WeaponClasses.Any(c => c.Id == classId);
    }
}
