﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Identity.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        
        Task<ApplicationUser> GetUsersByIdAsync(string id);
        
        Task<IEnumerable<Claim>> GetClaimsByIdUserAsync(ApplicationUser user);

        Task<IEnumerable<string>> GetRolesByIdUserAsync(ApplicationUser user);

        Task<IEnumerable<string>> GetRolesAsync();

        Task<IEnumerable<string>> GetClaimsAsync();

        Task ClearUserAsync(ApplicationUser user);

        Task SaveUserAsync(ApplicationUser user);

        Task AddRolesAndClaimsUserAsync(ApplicationUser user, IList<string> roles, IList<string> claims);

        Task<Tuple<bool, string[]>> SetPasswordAsync(ApplicationUser user, string newPassword);
    }
}
