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

            public const string DefaultImageUrl = "https://w7.pngwing.com/pngs/552/585/png-transparent-computer-icons-rifle-weapon-firearm-pistol-military-rifle-icon-miscellaneous-angle-hand-thumbnail.png";
        }

        public class Armor
        {
            public const int MaxNameLength = 70;
            public const int MinNameLength = 5;
            public const int MaxIntrinsicNameLength = 40;
            public const int MinIntrinsicNameLength = 5;
            public const int MaxIntrinsicDescriptionLength = 500;
            public const int MinIntrinsicDescriptionLength = 10;

            public const string DefaultImageUrl = "https://image.flaticon.com/icons/png/512/2147/2147193.png";
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
