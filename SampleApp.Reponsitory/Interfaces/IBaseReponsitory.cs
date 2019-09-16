// ***********************************************************************
// Assembly         : SampleApp.Reponsitory
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="IBaseReponsitory.cs" company="SampleApp.Reponsitory">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApp.Reponsitory.Intefaces
{
    /// <summary>
    /// Interface IBaseReponsitory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseReponsitory<T> where T : class
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Queries the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Firsts the or default asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Singles the or default asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
    }
}
