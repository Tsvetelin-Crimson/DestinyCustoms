namespace DestinyCustoms.Common
{
    public class WebConstants
    {
        public const string adminRoleName = "Administrator";
        public const string adminAreaName = "Admin";

        public const int HomePageNumberOfItems = 6;
        public const int AdminHomePageNumberOfItems = 20;

        public class Cache
        {
            public const string LatestWeaponsCacheKey = nameof(LatestWeaponsCacheKey); 
            public const string LatestArmorsCacheKey = nameof(LatestArmorsCacheKey);
        }
    }
}
