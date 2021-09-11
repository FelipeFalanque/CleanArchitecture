using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Identity.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
    }
}
