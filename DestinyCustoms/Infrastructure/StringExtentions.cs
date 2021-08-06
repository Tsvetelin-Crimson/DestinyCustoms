using Microsoft.AspNetCore.Mvc;

namespace DestinyCustoms.Infrastructure
{
    public static class StringExtentions
    {
        public static string RemoveControllerFromString(this string baseString)
            => baseString.Replace(nameof(Controller), string.Empty);
    }
}
