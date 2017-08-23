using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Interfaces;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Services
{
    public partial class BaseService<T> : IBaseService<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }
        #region CREATE
        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (_repository.IsExist(entity))
                throw new ArgumentException("This entity is already exist.");

            return _repository.Insert(entity);
        }
        /// <summary>
        /// Create entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (await _repository.IsExistAsync(entity))
                throw new ArgumentException("This entity is already exist.");

            return await _repository.InsertAsync(entity);
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!_repository.IsExist(entity))
                throw new ArgumentException("This entity is not exist.");

            _repository.Update(entity);
        }
        /// <summary>
        /// Update entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!await _repository.IsExistAsync(entity))
                throw new ArgumentException("This entity is not exist.");

            await _repository.UpdateAsync(entity);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!_repository.IsExist(id))
                throw new ArgumentException("Entity is not exist");

            _repository.Delete(id);
        }
        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!await _repository.IsExistAsync(id))
                throw new ArgumentException("Entity is not exist");

            await _repository.DeleteAsync(id);
        }
        #endregion

        #region GET
        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!_repository.IsExist(id))
                throw new ArgumentException("This entity is not exist.");

            return _repository.GetById(id);
        }
        /// <summary>
        /// Get entity by Id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!await _repository.IsExistAsync(id))
                throw new ArgumentException("This entity is not exist.");

            return await _repository.GetByIdAsync(id);
        }
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
        /// <summary>
        /// Get all entities async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        #endregion

        /// <summary>
        /// Check entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool IsExist(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return _repository.IsExist(entity);
        }
        /// <summary>
        /// Check entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsExistAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.IsExistAsync(entity);
        }
        /// <summary>
        /// Check entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool IsExist(Guid id)
        {
            return _repository.IsExist(id);
        }
        /// <summary>
        /// Check entity async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsExistAsync(Guid id)
        {
            return await _repository.IsExistAsync(id);
        }
    }
}
