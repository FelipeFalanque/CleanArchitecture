using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repository;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dataset;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (result == null)
                return false;

            _dataset.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T item)
        {
            _dataset.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> SelectAsync(int id)
        {
            return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            return await _dataset.ToListAsync();
        }

        public async Task<T> UpdateAsync(T item)
        {
            _dataset.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
