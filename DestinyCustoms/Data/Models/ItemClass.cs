using System.Collections.Generic;

namespace DestinyCustoms.Data.Models
{
    public class ItemClass
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Exotic> Exotics { get; set; }
    }
}
