using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly DestinyCustomsDbContext db;

        public CommentsService(DestinyCustomsDbContext db) 
            => this.db = db;

        public int Create(string content, int weaponId, string userId)
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

        public IEnumerable<CommentServiceModel> GetByWeaponId(int WeaponId)
                => db.Comments
                    .Where(c => c.WeaponId == WeaponId)
                    .Select(c => new CommentServiceModel
                    {
                        Content =c.Content,
                        UserUsername = c.User.UserName,
                    })
                    .ToList();
    }
}
