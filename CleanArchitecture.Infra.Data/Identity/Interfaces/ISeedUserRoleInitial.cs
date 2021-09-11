using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Identity.Interfaces
{
    public interface ISeedUserRoleInitial
    {
        Task SeedUsers();
        Task SeedRoles();
    }
}
