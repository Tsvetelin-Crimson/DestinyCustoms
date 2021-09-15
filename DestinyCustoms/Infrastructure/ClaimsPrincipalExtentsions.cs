using System.Security.Claims;

namespace DestinyCustoms.Infrastructure
{
    public static class ClaimsPrincipalExtentsions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
