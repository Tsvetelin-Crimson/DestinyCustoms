using AutoMapper;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Comments.Models;
using DestinyCustoms.Services.Weapons.Models;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace DestinyCustoms.Services
{
    public class ServicesMapperProfile : Profile
    {
        public ServicesMapperProfile()
        {
            this.CreateMap<WeaponClass, WeaponClassServiceModel>();

            this.CreateMap<ExoticWeapon, WeaponServiceModel>()
                .ForMember(w => w.ClassName, cfg => cfg.MapFrom(w => w.WeaponClass.Name));

            this.CreateMap<ExoticWeapon, DetailsWeaponServiceModel>()
                .ForMember(w => w.ClassName, cfg => cfg.MapFrom(w => w.WeaponClass.Name))
                .ForMember(w => w.ClassId, cfg => cfg.MapFrom(w => w.WeaponClass.Id));

            this.CreateMap<Comment, CommentServiceModel>()
                .ForMember(c => c.Replies, cfg => cfg.MapFrom(c => c.Replies.Where(r => r.CommentId == c.Id))); // add select here

            this.CreateMap<Reply, ReplyServiceModel>();

        }
    }
}
