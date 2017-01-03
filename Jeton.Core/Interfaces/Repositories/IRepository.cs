using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        #region INSERT

        /// <summary>
        /// Marks an entity as new
        /// </summary>
        /// <param name="entity"></param>
        T Insert(T entity);

        /// <summary>
        /// Marks an entity as new async
        /// </summary>
        /// <param name="entity"></param>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Insert entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        Task InsertAsync(IEnumerable<T> entities);

        #endregion

        #region UPDATE

        /// <summary>
        /// Marks an entity as modified
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Marks an entity as modified async
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAsync(T entity);

        #endregion

        #region DELETE
        /// <summary>
        /// Marks an entity to be removed
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Marks an entity to be removed
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Delete many entity by linq expression query
        /// </summary>
        /// <param name="where"></param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Delete many entity by linq expression query async
        /// </summary>
        /// <param name="where"></param>
        Task DeleteAsync(Expression<Func<T, bool>> where);
        #endregion

        #region SELECT

        /// <summary>
        /// Get an entity by int id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Guid id);

        /// <summary>
        /// Get an entity by int id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Get many entity by filter
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        /// <summary>
        /// Get many entity by filter async
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        
        /// <summary>
        /// Get all entities async
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }
        
        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
        #endregion
    }
}
