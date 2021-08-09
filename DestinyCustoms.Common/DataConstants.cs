namespace DestinyCustoms.Common
{
    public class DataConstants
    {
        public class Weapon
        {
            public const int MaxNameLength = 50;
            public const int MinNameLength = 5;
            public const int MaxIntrinsicNameLength = 40;
            public const int MinIntrinsicNameLength = 5;
            public const int MaxIntrinsicDescriptionLength = 500;
            public const int MinIntrinsicDescriptionLength = 10;
            public const int MaxCatalystNameLength = 40;
            public const int MinCatalystNameLength = 5;
            public const int MaxCatalystCompletionLength = 100;
            public const int MinCatalystCompletionLength = 10;
            public const int MaxCatalystEffectLength = 100;
            public const int MinCatalystEffectLength = 10;

            public const string DefaultImageUrl = "https://lh3.googleusercontent.com/proxy/Ed5-kJ4saHYAPos1tHFyVqarkW1QPOOjL5oYfV94R4GhN3gX7rBR9HxSEJ2WjPt1HoBX54MezNUooQqMQu-TJhExgg";
        }

        public class Armor
        {
            public const int MaxNameLength = 70;
            public const int MinNameLength = 5;
            public const int MaxIntrinsicNameLength = 40;
            public const int MinIntrinsicNameLength = 5;
            public const int MaxIntrinsicDescriptionLength = 500;
            public const int MinIntrinsicDescriptionLength = 10;

            public const string DefaultImageUrl = "https://d1nhio0ox7pgb.cloudfront.net/_img/i_collection_png/512x512/plain/armour.png";
        }

        public class WeaponClass
        {
            public const int MaxNameLength = 30;
        }

        public class Comment
        {
            public const int MaxContentLength = 500;
            public const int MinContentLength = 1;
        }
    }
}
