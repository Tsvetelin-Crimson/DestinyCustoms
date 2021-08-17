namespace DestinyCustoms.Services.Weapons.Models
{
    public class WeaponDetailsServiceModel
    {
        public string Id { get; init; }

        public string Name { get; set; }

        public string IntrinsicName { get; set; }

        public string IntrinsicDescription { get; set; }

        public string CatalystName { get; set; }

        public string CatalystCompletionRequirement { get; set; }

        public string CatalystEffect { get; set; }

        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; init; }

        public string UserUsername { get; set; }
    }
}
