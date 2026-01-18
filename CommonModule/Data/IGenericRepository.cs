using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CommonModule.Data
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    }
}
