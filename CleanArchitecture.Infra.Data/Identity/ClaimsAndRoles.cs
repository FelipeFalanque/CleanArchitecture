using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Identity
{
    public static class ClaimsAndRoles
    {
        public static IList<string> Roles = new List<string>() { "User", "Admin" };

        public static IList<string> Claims = new List<string>() { "Add", "Edit", "Consult", "Delete" };
    }
}
