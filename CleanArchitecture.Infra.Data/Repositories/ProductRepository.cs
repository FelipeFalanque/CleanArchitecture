using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repository;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private DbSet<Product> _dataset;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _dataset = context.Set<Product>();
        }

        public async Task<Product> SelectEagerAsync(int id)
        {
            return await _dataset.Include("Category").SingleOrDefaultAsync(p => p.Id.Equals(id));
        }
        
        public async Task<IEnumerable<Product>> SelectByCategoryIdAsync(int idCategory)
        {
            return await _dataset.Where(p => p.CategoryId == idCategory).ToListAsync();
        }
    }
}
