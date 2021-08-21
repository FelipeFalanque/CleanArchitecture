using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<T> SelectAsync(int id);
        Task<IEnumerable<T>> SelectAsync();
        Task<bool> ExistAsync(int id);
    }
}
