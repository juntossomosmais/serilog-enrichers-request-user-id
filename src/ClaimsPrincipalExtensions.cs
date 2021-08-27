using System.Linq;
using System.Security.Claims;

namespace Serilog.Enrichers.RequestUserId
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claims = claimsPrincipal.FindAll(t => t.Type == "cid" || t.Type == "uid");

            if (!claims.Any()) return string.Empty;

            return claims.FirstOrDefault().Value;
        }
    }
}