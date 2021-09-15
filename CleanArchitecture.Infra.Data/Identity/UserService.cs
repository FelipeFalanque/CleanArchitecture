using CleanArchitecture.Infra.Data.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    }
}
