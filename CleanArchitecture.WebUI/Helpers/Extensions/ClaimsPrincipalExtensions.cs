﻿using System;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Helpers.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        /// Use in Controller
        /// var idUser = User.GetUserId();
        ///
    }
}
