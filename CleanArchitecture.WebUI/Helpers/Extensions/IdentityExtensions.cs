using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

            var name = identity.Name;

            return string.Format("{0} {1}", "Hello", name).Trim();

        }
    }
}
