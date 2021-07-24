using DestinyCustoms.Services.Comments.Models;
using System.Collections.Generic;

namespace DestinyCustoms.Services.Comments
{
    public interface ICommentsService
    {
        int Create(string content, int weaponId, string userId);

        IEnumerable<CommentServiceModel> GetByWeaponId(int WeaponId);
    }
}
