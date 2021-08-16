using System.Linq;
using AutoMapper;
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

            // Weapon Maps
            this.CreateMap<ExoticWeapon, WeaponValidationServiceModel>();
            this.CreateMap<ExoticWeapon, WeaponServiceModel>()
                .ForMember(ws => ws.ClassName, cfg => cfg.MapFrom(w => w.WeaponClass.Name));
            this.CreateMap<ExoticWeapon, WeaponDetailsServiceModel>()
                .ForMember(wds => wds.ClassName, cfg => cfg.MapFrom(w => w.WeaponClass.Name))
                .ForMember(wds => wds.ClassId, cfg => cfg.MapFrom(w => w.WeaponClass.Id));

            // Armor Maps
            this.CreateMap<ExoticArmor, ArmorValidationServiceModel>();
            this.CreateMap<ExoticArmor, ArmorServiceModel>()
                .ForMember(a => a.ClassName, cfg => cfg.MapFrom(a => a.CharacterClass.ToString()));
            this.CreateMap<ExoticArmor, ArmorDetailsServiceModel>()
                .ForMember(a => a.ClassName, cfg => cfg.MapFrom(a => a.CharacterClass.ToString()));

            //Comments and Replies Maps
            this.CreateMap<Comment, CommentServiceModel>()
                .ForMember(c => c.Replies, cfg => cfg.MapFrom(c => c.Replies.Where(r => r.CommentId == c.Id)));
            this.CreateMap<Reply, ReplyServiceModel>();

            // Extra Maps
            this.CreateMap<WeaponClass, WeaponClassServiceModel>();

            // Admin Maps
            this.CreateMap<ExoticWeapon, AdminMostRecentServiceModel>();
            this.CreateMap<ExoticArmor, AdminMostRecentServiceModel>();
        }
    }
}
