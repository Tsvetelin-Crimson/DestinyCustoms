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
        public int CreateWeaponComment(string content, string weaponId, string userId)
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

        public int CreateArmorComment(string content, string armorId, string userId)
        {
            var comment = new Comment()
            {
                Content = content,
                ArmorId = armorId,
                UserId = userId,
            };
            db.Comments.Add(comment);
            db.SaveChanges();

            return comment.Id;
        }

        public int CreateReply(string content, int commentId, string userId)
        {
            var reply = new Reply()
            {
                Content = content,
                CommentId = commentId,
                UserId = userId,
            };

            db.Replies.Add(reply);
            db.SaveChanges();

            return reply.Id;
        }

        public CommentServiceModel GetById(int Id)
            => db.Comments
                    .Where(c => c.Id == Id)
                    .ProjectTo<CommentServiceModel>(this.mapper)
                    .FirstOrDefault();

        public IEnumerable<CommentServiceModel> GetByWeaponId(string weaponId)
            => db.Comments
                    .Where(c => c.WeaponId == weaponId)
                    .ProjectTo<CommentServiceModel>(this.mapper)
                    .ToList();

        public IEnumerable<CommentServiceModel> GetByArmorId(string armorId)
            => db.Comments
                    .Where(c => c.ArmorId == armorId)
                    .ProjectTo<CommentServiceModel>(this.mapper)
                    .ToList();
    }
}
