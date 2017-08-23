using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Interfaces.Services
{
    public interface IBaseService<T> where T : IEntity
    {
        #region CREATE
        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Create(T entity);
        /// <summary>
        /// Create entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> CreateAsync(T entity);
        #endregion

        #region UPDATE
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Update entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
        #endregion

        #region DELETE
        /// <summary>
        /// Delete entity by Id
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);
        /// <summary>
        /// Delete entity by Id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
        #endregion

        #region GET
        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Guid id);
        /// <summary>
        /// Get entity by Id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Get all async
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
        #endregion

        /// <summary>
        /// Check entity is exist
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsExist(T entity);
        /// <summary>
        /// Check entity is exist async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(T entity);

        /// <summary>
        /// Check entity is exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsExist(Guid id);
        /// <summary>
        /// Check entity is exist async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(Guid id);
    }
}
