using System.Collections.Generic;
using DestinyCustoms.Services.Comments.Models;

namespace DestinyCustoms.Services.Comments
{
    public interface ICommentsService
    {
        int Create(string content, string weaponId, string userId);

        IEnumerable<CommentServiceModel> GetByWeaponId(string WeaponId);
    }
}
