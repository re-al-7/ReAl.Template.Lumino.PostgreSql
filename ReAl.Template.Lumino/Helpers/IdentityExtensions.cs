using System.Security.Claims;
using System.Security.Principal;

namespace ReAl.Template.Lumino.Helpers
{
    public static class IdentityExtensions
    {
        public static string GetGivenName(this IIdentity identity)
        {
            if (identity == null)
                return null;

            return (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.GivenName);
        }

        internal static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }
    }
}
