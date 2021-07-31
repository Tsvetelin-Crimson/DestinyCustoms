namespace DestinyCustoms.Services.Armors.Models
{
    public class ArmorDetailsServiceModel
    {
        public string Id { get; init; }

        public string Name { get; set; }

        public string IntrinsicName { get; set; }

        public string IntrinsicDescription { get; set; }

        public string ClassName { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; init; }
    }
}
