using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApp.Reponsitory
{
    public interface IBaseReponsitory<T> where T : class
    {
        IQueryable<T> GetAll();

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);

        Task<T> GetByIdAsync(Guid id);

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
    }
}
