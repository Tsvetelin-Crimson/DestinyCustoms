using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Comments.Models;

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
                DateCreated = DateTime.UtcNow,
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
                DateCreated = DateTime.UtcNow,
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
                DateCreated = DateTime.UtcNow,
                CommentId = commentId,
                UserId = userId,
            };

            db.Replies.Add(reply);
            db.SaveChanges();

            return reply.Id;
        }

        public bool DeleteComment(int id)
        {
            var comment = db.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return false;
            }

            var replies = db.Replies.Where(r => r.CommentId == comment.Id);

            db.Replies.RemoveRange(replies);
            db.Comments.Remove(comment);
            db.SaveChanges();

            return true;
        }

        public bool DeleteReply(int id)
        {
            var reply = db.Replies.FirstOrDefault(c => c.Id == id);
            if (reply == null)
            {
                return false;
            }

            db.Replies.Remove(reply);
            db.SaveChanges();

            return true;
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

        public ReplyServiceModel GetReplyById(int Id)
            => db.Replies
                    .Where(r => r.Id == Id)
                    .ProjectTo<ReplyServiceModel>(this.mapper)
                    .FirstOrDefault();
    }
}
