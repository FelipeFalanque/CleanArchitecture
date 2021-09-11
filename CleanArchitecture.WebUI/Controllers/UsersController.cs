using CleanArchitecture.Infra.Data.Identity.Interfaces;
using CleanArchitecture.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    }
}
