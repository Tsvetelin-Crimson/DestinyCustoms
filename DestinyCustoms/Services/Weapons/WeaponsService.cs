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
                    UserId = w.UserId,
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
                UserId = w.UserId,
            })
            .Take(6)
            .ToList();

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
                        ClassId = w.WeaponClassId,
                        ImageUrl = w.ImageURL,
                        UserId = w.UserId,
                    })
                    .FirstOrDefault();

        public int GetIdById(int id)
            => db.Weapons
                .Where(w => w.Id == id)
                .Select(w => w.Id)
                .FirstOrDefault();

        public WeaponValidationServiceModel GetIdAndUserIdById(int id)
            => db.Weapons
                .Where(w => w.Id == id)
                .Select(w => new WeaponValidationServiceModel { WeaponId = w.Id, UserId = w.UserId })
                .FirstOrDefault();

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


        public int Edit(
            int id,
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
            weapon.WeaponIntrinsicName = intrinsicName;
            weapon.WeaponIntrinsicDescription = intrinsicDescription;
            weapon.CatalystName = catalystName;
            weapon.CatalystCompletionRequirement = catalystCompletionRequirement;
            weapon.CatalystEffect = catalystEffect;
            weapon.WeaponClassId = classId;
            weapon.ImageURL = imageUrl;
            weapon.DateModified = DateTime.UtcNow;

            db.SaveChanges();

            return id;
        }

        public void Delete(int id)
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
