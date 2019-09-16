using Microsoft.EntityFrameworkCore;
using SampleApp.Infrastructure;
using SampleApp.Reponsitory.Intefaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApp.Reponsitory
{
    public class BaseReponsitory<T> : IBaseReponsitory<T> where T : class
    {
        private readonly SampleContext _context = null;

        public BaseReponsitory(SampleContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Set<T>().FirstOrDefaultAsync();
            }

            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Set<T>().SingleOrDefaultAsync();
            }

            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }
    }
}
