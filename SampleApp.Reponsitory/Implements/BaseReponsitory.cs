// ***********************************************************************
// Assembly         : SampleApp.Reponsitory
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="BaseReponsitory.cs" company="SampleApp.Reponsitory">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using SampleApp.Infrastructure;
using SampleApp.Reponsitory.Intefaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApp.Reponsitory
{
    /// <summary>
    /// Class BaseReponsitory.
    /// Implements the <see cref="SampleApp.Reponsitory.Intefaces.IBaseReponsitory{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SampleApp.Reponsitory.Intefaces.IBaseReponsitory{T}" />
    public class BaseReponsitory<T> : IBaseReponsitory<T> where T : class
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly SampleContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReponsitory{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseReponsitory(SampleContext context)
        {
            _context = context;
        }

        /// <summary>
        /// add as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Queries the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// first or default as an asynchronous operation.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Set<T>().FirstOrDefaultAsync();
            }

            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// single or default as an asynchronous operation.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task&lt;T&gt;.</returns>
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
