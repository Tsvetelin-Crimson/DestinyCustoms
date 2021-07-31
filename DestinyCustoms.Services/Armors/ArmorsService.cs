using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DestinyCustoms.Common.Enums;
using DestinyCustoms.Data;
using DestinyCustoms.Services.Armors.Models;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Services.Armors
{
    public class ArmorsService : IArmorsService
    {
        private readonly DestinyCustomsDbContext db;
        private readonly IConfigurationProvider mapper;

        public ArmorsService(DestinyCustomsDbContext db, IMapper mapper)
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
        public IEnumerable<ArmorServiceModel> MostRecentlyCreated()
            => db.Armors
            .OrderByDescending(w => w.DateCreated)
            .ProjectTo<ArmorServiceModel>(this.mapper)
            .Take(6)
            .ToList();


        public string Create(
            string name, 
            string intrinsicName, 
            string intrinsicDescription,
            CharacterClass classEnum, 
            string imageUrl,
            string userId)
        {
            // TODO: Add default Image URL If null
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


        public IEnumerable<string> AllClassNames()
            => Enum.GetNames(typeof(CharacterClass)).ToList();
    }
}
