using CleanArchitecture.Infra.Data.Identity.Interfaces;
using CleanArchitecture.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            var usersVM = users.Select(u => new UserViewModel(u.Id, u.Name, u.Email, u.UserName));

            return View(usersVM);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userService.GetUsersByIdAsync(id);

            if (user == null)
                return NotFound();

            var claimsUser = await _userService.GetClaimsByIdUserAsync(user);
            var rolesUser = await _userService.GetRolesByIdUserAsync(user);

            var claims = await _userService.GetClaimsAsync();
            var roles = await _userService.GetRolesAsync();

            var userVM = new UserViewModel(user.Id, user.Name, user.Email, user.UserName);
            userVM.Roles.AddRange(roles.Select(r => new RoleViewModel() { Name = r, Has = false }));
            userVM.Claims.AddRange(claims.Select(c => new ClaimViewModel() { Name = c, Has = false }));

            if (rolesUser != null && rolesUser.Count() > 0)
                foreach (var role in rolesUser)
                    userVM.Roles.First(r => r.Name == role).Has = true;

            if (claimsUser != null && claimsUser.Count() > 0)
                foreach (var claim in claimsUser)
                    userVM.Claims.First(c => c.Name == claim.Value).Has = true;

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUsersByIdAsync(userVM.Id);

                if (user == null)
                    return NotFound();

                user.UserName = userVM.UserName;
                user.Name = userVM.Name;

                await _userService.ClearUserAsync(user);
                await _userService.SaveUserAsync(user);

                var claims = userVM.Claims.Where(c => c.Has).Select(i => i.Name).ToList();
                var roles = userVM.Roles.Where(c => c.Has).Select(i => i.Name).ToList();

                await _userService.AddRolesAndClaimsUserAsync(user, roles, claims);

                return RedirectToAction(nameof(Index));
            }

            return View(userVM);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userService.GetUsersByIdAsync(id);
            var claims = await _userService.GetClaimsByIdUserAsync(user);
            var roles = await _userService.GetRolesByIdUserAsync(user);

            if (user == null)
                return NotFound();

            ViewBag.Claims = claims;
            ViewBag.Roles = roles;

            return View(new UserViewModel(user.Id, user.Name, user.Email, user.UserName));
        }

        [HttpPost]
        public async Task<IActionResult> SetPassword(string id, string password)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userService.GetUsersByIdAsync(id);

            if (user == null)
                return NotFound();

            var result = await _userService.SetPasswordAsync(user, password);

            if (result.Item2 != null && result.Item2.Length > 0)
                return Problem(string.Join("," ,result.Item2));

            return Ok();
        }
    }
}
