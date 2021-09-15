using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserViewModel(string id, string name, string email, string userName)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.UserName = userName;
            this.Claims = new List<ClaimViewModel>();
            this.Roles = new List<RoleViewModel>();
        }

        public UserViewModel()
        {
            this.Id = "";
            this.Name = "";
            this.Email = "";
            this.UserName = "";
            this.Claims = new List<ClaimViewModel>();
            this.Roles = new List<RoleViewModel>();
        }

        public List<ClaimViewModel> Claims { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }

    public class ClaimViewModel
    {
        public string Name { get; set; }
        public bool Has { get; set; }
    }

    public class RoleViewModel
    {
        public string Name { get; set; }
        public bool Has { get; set; }
    }
}
