using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Comments.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace DestinyCustoms.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly DestinyCustomsDbContext db;
        private readonly IConfigurationProvider mapper;


        public CommentsService(DestinyCustomsDbContext db, IMapper mapper)
        { 
            this.db = db;
            this.mapper = mapper.ConfigurationProvider;
        }
        public int Create(string content, string weaponId, string userId)
        {
            var comment = new Comment()
            {
                Content = content,
                WeaponId = weaponId,
                UserId = userId,
            };
            db.Comments.Add(comment);
            db.SaveChanges();

            return comment.Id;
        }

        public IEnumerable<CommentServiceModel> GetByWeaponId(string WeaponId)
                => db.Comments
                    .Where(c => c.WeaponId == WeaponId)
                    .ProjectTo<CommentServiceModel>(this.mapper)
                    .ToList();
    }
}
