﻿using DestinyCustoms.Data.Models;
using System.Collections.Generic;

namespace DestinyCustoms.Tests.Data
{
    public static class Comments
    {
        public static Comment OneCommentWithSetId(int id, string armorId = null, string weapondId = null)
            => new()
            {
                Id = id,
                WeaponId = weapondId,
                ArmorId = armorId,
            };

    }
}
