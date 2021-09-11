using CleanArchitecture.Infra.Data.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Tuple<bool, string[]>> Authenticate(string email, string password)
        {
            string[] errors = null;

            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                List<string> errorsTemp = new List<string>();

                if (result.IsLockedOut)
                {
                    errorsTemp.Add("Lockout");
                }
                else if (result.IsNotAllowed)
                {
                    errorsTemp.Add("Not Allowed");
                }
                else
                {
                    errorsTemp.Add("Invalid login attempt.");
                }

                errors = errorsTemp.ToArray();
            }

            return new Tuple<bool, string[]>(result.Succeeded, errors);
        }
        public async Task<Tuple<bool, string[]>> RegisterUser(string email, string password)
        {
            string[] errors = null;

            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded)
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
            else
                errors = result.Errors.Select(e => e.Description).ToArray();

            return new Tuple<bool, string[]>(result.Succeeded, errors);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
