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


        public IEnumerable<CommentServiceModel> GetByWeaponId(string WeaponId)
            => db.Comments
                    .Where(c => c.WeaponId == WeaponId)
                    .Select(c => new CommentServiceModel
                    {
                        Id = c.Id,
                        Content = c.Content,
                        UserUsername = c.User.UserName,
                        Replies = c.Replies
                        .Where(r => r.CommentId == c.Id)
                        .Select(r => new ReplyServiceModel
                        {
                            Content = r.Content,
                            UserUsername = r.User.UserName,
                        })
                        .ToList(),
                    })
                    //.ProjectTo<CommentServiceModel>(this.mapper)
                    .ToList();
        
    }
}
