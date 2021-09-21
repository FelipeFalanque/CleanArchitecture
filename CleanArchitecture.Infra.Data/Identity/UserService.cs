using CleanArchitecture.Infra.Data.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        
        public async Task<ApplicationUser> GetUsersByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Claim>> GetClaimsByIdUserAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }
        
        public async Task<IEnumerable<string>> GetRolesByIdUserAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        
        public async Task<IEnumerable<string>> GetRolesAsync()
        {
            return ClaimsAndRoles.Roles;
        }
        
        public async Task<IEnumerable<string>> GetClaimsAsync()
        {
            return ClaimsAndRoles.Claims;
        }

        public async Task ClearUserAsync(ApplicationUser user)
        {
            var rolesForUser = await _userManager.GetRolesAsync(user);
            var claimsForUser = await _userManager.GetClaimsAsync(user);

            if (rolesForUser.Count() > 0)
                foreach (var item in rolesForUser.ToList())
                    await _userManager.RemoveFromRoleAsync(user, item);

            if (claimsForUser.Count() > 0)
                foreach (Claim claim in claimsForUser)
                    await _userManager.RemoveClaimAsync(user, claim);
        }
        
        public async Task SaveUserAsync(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }
        
        public async Task AddRolesAndClaimsUserAsync(ApplicationUser user, IList<string> roles, IList<string> claims)
        {
            foreach (var role in roles)
                // Associate the role with the new user 
                await _userManager.AddToRoleAsync(user, role);

            foreach (var claim in claims)
                // Associate the role with the new user 
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.IsPersistent, claim));
        }
        
        public async Task<Tuple<bool, string[]>> SetPasswordAsync(ApplicationUser user, string newPassword)
        {
            var errors = new List<string>();

            // Removendo a senha Atual do usuário
            var resultR = await _userManager.RemovePasswordAsync(user);

            if (resultR.Succeeded)
            {
                // Adicionando uma nova senha para o usuário
                var resultA = await _userManager.AddPasswordAsync(user, newPassword);

                if (resultA.Succeeded)
                    return new Tuple<bool, string[]>(true, errors.ToArray());
                else
                    errors.AddRange(resultA.Errors.Select(e => e.Description).ToList());
            }
            else
                errors.AddRange(resultR.Errors.Select(e => e.Description).ToList());

            return new Tuple<bool, string[]>(true, errors.ToArray());
        }
    }
}
