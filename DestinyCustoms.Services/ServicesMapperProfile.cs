using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Comments.Models;
using DestinyCustoms.Services.Weapons.Models;
using DestinyCustoms.Services.Armors.Models;
using DestinyCustoms.Services.CommonModels;

namespace DestinyCustoms.Services
{
    public class ServicesMapperProfile : Profile
    {
        public ServicesMapperProfile()
        {
            this.CreateMap<WeaponClass, WeaponClassServiceModel>();

            this.CreateMap<ExoticWeapon, WeaponServiceModel>()
                .ForMember(w => w.ClassName, cfg => cfg.MapFrom(w => w.WeaponClass.Name));
            this.CreateMap<ExoticWeapon, WeaponDetailsServiceModel>()
                .ForMember(w => w.ClassName, cfg => cfg.MapFrom(w => w.WeaponClass.Name))
                .ForMember(w => w.ClassId, cfg => cfg.MapFrom(w => w.WeaponClass.Id));

            this.CreateMap<ExoticWeapon, AdminMostRecentServiceModel>();
            this.CreateMap<ExoticArmor, AdminMostRecentServiceModel>();

            this.CreateMap<ExoticArmor, ArmorValidationServiceModel>();
            this.CreateMap<ExoticArmor, ArmorServiceModel>()
                .ForMember(a => a.ClassName, cfg => cfg.MapFrom(a => a.CharacterClass.ToString()));
            this.CreateMap<ExoticArmor, ArmorDetailsServiceModel>()
                .ForMember(a => a.ClassName, cfg => cfg.MapFrom(a => a.CharacterClass.ToString()));


            this.CreateMap<Comment, CommentServiceModel>()
                .ForMember(c => c.Replies, cfg => cfg.MapFrom(c => c.Replies.Where(r => r.CommentId == c.Id))); // add select here

            this.CreateMap<Reply, ReplyServiceModel>();

        }
    }
}
