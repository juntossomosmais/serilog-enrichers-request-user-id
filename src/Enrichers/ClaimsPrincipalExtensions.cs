using System.Linq;
using System.Security.Claims;

namespace Serilog.Enrichers
{
    internal static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claims = claimsPrincipal.FindAll(t => t.Type is "cid" or "uid");

            if (!claims.Any()) return string.Empty;

            return claims.FirstOrDefault().Value;
        }
    }
}