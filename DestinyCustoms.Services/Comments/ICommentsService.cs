using System.Collections.Generic;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Services.Comments
{
    public interface ICommentsService
    {
        int CreateWeaponComment(string content, string weaponId, string userId);

        int CreateArmorComment(string content, string armorId, string userId);

        int CreateReply(string content, int commentId, string userId);

        public bool DeleteComment(int id);

        public bool DeleteReply(int id);

        CommentServiceModel GetById(int Id);

        IEnumerable<CommentServiceModel> GetByWeaponId(string WeaponId);

        IEnumerable<CommentServiceModel> GetByArmorId(string armorId);
    }
}
