namespace DestinyCustoms.Data.Models.ApplicationModels
{
    using System.Collections.Generic;

    using DestinyCustoms.Data.Common.Models;

    // TODO: Maybe change to BaseModel (no need for deleteon etc)
    public class ItemClass : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public ICollection<Exotic> Exotics { get; set; }
    }
}
