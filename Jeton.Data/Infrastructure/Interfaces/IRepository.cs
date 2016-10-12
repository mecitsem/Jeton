using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Marks an entity as new
        /// </summary>
        /// <param name="entity"></param>
        T Insert(T entity);
        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);
        /// <summary>
        /// Marks an entity as modified
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Marks an entity to be removed
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        void Delete(Expression<Func<T, bool>> where);
        /// <summary>
        /// Get an entity by int id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Guid id);
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }
        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
