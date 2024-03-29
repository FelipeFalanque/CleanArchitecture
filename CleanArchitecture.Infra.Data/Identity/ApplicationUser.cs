﻿using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infra.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
