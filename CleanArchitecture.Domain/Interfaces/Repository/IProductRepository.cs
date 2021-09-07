using CleanArchitecture.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> SelectEagerAsync(int id);

        Task<IEnumerable<Product>> SelectByCategoryIdAsync(int idCategory);

    }
}
