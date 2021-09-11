using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Identity.Interfaces
{
    public interface IAuthenticate
    {
        Task<Tuple<bool, string[]>> Authenticate(string email, string password);

        Task<Tuple<bool, string[]>> RegisterUser(string email, string password);

        Task Logout();

    }
}
