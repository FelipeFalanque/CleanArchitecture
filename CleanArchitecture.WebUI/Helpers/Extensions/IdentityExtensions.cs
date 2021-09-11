using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Helpers.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetIdentityName(this IIdentity identity)
        {
            if (identity == null)
                return null;

            /*
            
            In your SignInAsync method (or, wherever you are creating the claims identity), add the GivenName and Surname claim types:

            identity.AddClaim(new Claim(ClaimTypes.GivenName, your_profile == null ? string.Empty : your_profile.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, your_profile == null ? string.Empty : your_profile.LastName));

            ---------------------------------------------------------------------------------------------------------------------------

            var first = (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.GivenName),
            var last = (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.Surname)

            return string.Format("{0} {1}", first, last).Trim();
            
            */

            return "Cesar Felipe";
        }
    }
}
