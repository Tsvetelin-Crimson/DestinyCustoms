using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DestinyCustoms.Common.Enums;
using DestinyCustoms.Data;
using DestinyCustoms.Services.Armors.Models;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.CommonModels;

namespace DestinyCustoms.Services.Armors
{
    using static Common.DataConstants.Armor;
    using static Common.WebConstants;
    public class ArmorsService : IArmorsService
    {
        private readonly DestinyCustomsDbContext db;
        private readonly IConfigurationProvider mapper;

        public ArmorsService(
            DestinyCustomsDbContext db, 
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper.ConfigurationProvider;
        }

        public ArmorsQueryServiceModel All(
            string searchTerm,
            string className,
            int armorsPerPage,
            int currentPage)
        {
            var armorsQuery = db.Armors.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                armorsQuery = armorsQuery.Where(a => a.Name.Contains(searchTerm));
            }
            var isClassNameValid = Enum.TryParse<CharacterClass>(className, out CharacterClass classEnum);

            if (isClassNameValid && className != "All")
            {
                armorsQuery = armorsQuery.Where(a => a.CharacterClass == classEnum);
            }

            var armors = armorsQuery
                .OrderByDescending(w => w.Id)
                .Skip(armorsPerPage * (currentPage - 1))
                .Take(armorsPerPage)
                .ProjectTo<ArmorServiceModel>(this.mapper)
                .ToList();

            return new ArmorsQueryServiceModel
            {
                Armors = armors,
                AllArmorsCount = armorsQuery.Count(),
            };
        }

        public List<ArmorServiceModel> AllUserOwned(string userId)
            => db.Armors
            .Where(w => w.UserId == userId)
            .ProjectTo<ArmorServiceModel>(this.mapper)
            .ToList();

        public ArmorDetailsServiceModel GetById(string id)
            => db.Armors
            .Where(a => a.Id == id)
            .ProjectTo<ArmorDetailsServiceModel>(this.mapper)
            .FirstOrDefault();

        public string GetIdById(string id)
            => db.Armors
            .Where(a => a.Id == id)
            .Select(a => a.Id)
            .FirstOrDefault();

        public ArmorValidationServiceModel GetIdAndUserIdById(string id)
        => db.Armors
            .Where(a => a.Id == id)
            .ProjectTo<ArmorValidationServiceModel>(this.mapper)
            .FirstOrDefault();

        public List<ArmorServiceModel> MostRecentlyCreated()
            => db.Armors
            .OrderByDescending(w => w.DateCreated)
            .ProjectTo<ArmorServiceModel>(this.mapper)
            .Take(HomePageNumberOfItems)
            .ToList();

        public List<AdminMostRecentServiceModel> AdminMostRecentlyModified()
            => db.Armors
                .OrderByDescending(w => w.DateCreated)
                .ProjectTo<AdminMostRecentServiceModel>(this.mapper)
                .Take(AdminHomePageNumberOfItems)
                .ToList();

        public string Create(
            string name, 
            string intrinsicName, 
            string intrinsicDescription,
            CharacterClass classEnum, 
            string imageUrl,
            string userId)
        {
            if (imageUrl == null)
            {
                imageUrl = DefaultImageUrl;
            }

            var armor = new ExoticArmor
            {
                Name = name,
                IntrinsicName = intrinsicName,
                IntrinsicDescription = intrinsicDescription,
                CharacterClass = classEnum,
                ImageURL = imageUrl,
                UserId = userId,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };

            db.Armors.Add(armor);
            db.SaveChanges();

            return armor.Id;
        }


        public string Edit(
            string id, 
            string name, 
            string intrinsicName, 
            string intrinsicDescription,
            CharacterClass classEnum,
            string imageUrl)
        {
            var weapon = db.Armors.Find(id);

            weapon.Name = name;
            weapon.IntrinsicName = intrinsicName;
            weapon.IntrinsicDescription = intrinsicDescription;
            weapon.CharacterClass = classEnum;
            weapon.ImageURL = imageUrl ?? DefaultImageUrl;
            weapon.DateModified = DateTime.UtcNow;

            db.SaveChanges();

            return id;
        }

        public void Delete(string id)
        {
            var armor = db.Armors
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (armor != null)
            {
                db.Armors.Remove(armor);
                db.SaveChanges();
            }
        }
        public List<string> AllClassNames()
            => Enum.GetNames(typeof(CharacterClass)).ToList();

        public (bool, CharacterClass) IsCharacterClassNameValid(string name)
        {
            var isClassCorrect = Enum.TryParse(name, out CharacterClass classEnum);

            return (isClassCorrect, classEnum);
        }
    }
}
