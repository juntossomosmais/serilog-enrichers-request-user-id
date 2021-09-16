using System.Linq;
using System.Security.Claims;

namespace Serilog.Enrichers
{
    internal static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claims = claimsPrincipal.FindAll(t => t.Type is "cid" or "uid");

            return claims.FirstOrDefault()?.Value ?? string.Empty;
        }
    }
}