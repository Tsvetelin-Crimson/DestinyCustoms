using System.Collections.Generic;
using DestinyCustoms.Services.CommonModels;

namespace DestinyCustoms.Areas.Admin.Models.Home
{
    public class MostRecentItemsViewModel
    {
        public List<AdminMostRecentServiceModel> Items { get; set; }

        public string ItemLocation { get; set; }
    }
}
