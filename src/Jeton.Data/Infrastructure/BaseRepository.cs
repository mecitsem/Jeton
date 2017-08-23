using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeton.Core.Common;
using Jeton.Core.Interfaces;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.DbContext;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Infrastructure
{
    public partial class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        #region Properties
        private JetonDbContext _dataContext;
        private IDbSet<T> _entities;


        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities => _entities ?? (_entities = DbContext.Set<T>());

        protected JetonDbContext DbContext => _dataContext ?? (_dataContext = DbFactory.Init());

        #endregion

        #region Ctor
        protected BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            return exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).Aggregate(string.Empty, (current, error) => current + ($"Property: {error.PropertyName} Error: {error.ErrorMessage}" + Environment.NewLine));
        }

        #endregion

        #region Methods

        #region INSERT
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                entity.Created = Constants.Now;
                entity.Modified = Constants.Now;

                var result = Entities.Add(entity);

                DbContext.Commit();

                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }
        /// <summary>
        /// Insert entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                entity.Created = Constants.Now;
                entity.Modified = Constants.Now;

                var result = Entities.Add(entity);

                await DbContext.CommitAsync();

                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }
        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                {
                    entity.Created = Constants.Now;
                    entity.Modified = Constants.Now;
                    Entities.Add(entity);
                }


                DbContext.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }
        /// <summary>
        /// Insert entities async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                {
                    entity.Created = Constants.Now;
                    entity.Modified = Constants.Now;
                    Entities.Add(entity);
                }


                await DbContext.CommitAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                entity.Modified = Constants.Now;

                Entities.Attach(entity);

                DbContext.Entry(entity).State = EntityState.Modified;

                DbContext.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                entity.Modified = Constants.Now;

                Entities.Attach(entity);

                DbContext.Entry(entity).State = EntityState.Modified;

                await DbContext.CommitAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {

            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                if (entity.GetType().GetProperty("IsDeleted") != null)
                {
                    var _entity = entity;

                    _entity.GetType().GetProperty("IsDeleted").SetValue(_entity, true);

                    Update(_entity);
                }
                else
                {
                    var dbEntityEntry = DbContext.Entry(entity);

                    if (dbEntityEntry.State != EntityState.Deleted)
                    {
                        dbEntityEntry.State = EntityState.Deleted;
                    }
                    else
                    {
                        Entities.Attach(entity);
                        Entities.Remove(entity);
                    }

                    DbContext.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }


        }
        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                if (entity.GetType().GetProperty("IsDeleted") != null)
                {
                    var _entity = entity;

                    _entity.GetType().GetProperty("IsDeleted").SetValue(_entity, true);

                    Update(_entity);
                }
                else
                {
                    var dbEntityEntry = DbContext.Entry(entity);

                    if (dbEntityEntry.State != EntityState.Deleted)
                    {
                        dbEntityEntry.State = EntityState.Deleted;
                    }
                    else
                    {
                        Entities.Attach(entity);
                        Entities.Remove(entity);
                    }

                    await DbContext.CommitAsync();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }

        }

        public virtual void Delete(Guid id)
        {
            var entity = GetById(id);

            Delete(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            await DeleteAsync(entity);
        }

        /// <summary>
        /// DELETE MANY
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = Entities.Where<T>(where).AsEnumerable();
            foreach (var obj in objects)
                Delete(obj);
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> @where)
        {
            var objects = Entities.Where<T>(where).AsEnumerable();
            foreach (var obj in objects)
                await DeleteAsync(obj);
        }
        #endregion

        #region SELECT
        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
        {
            return Entities.Find(id);
        }
        /// <summary>
        /// Get entity by Id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await Entities.FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        /// <summary>
        /// GET MANY
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return Entities.Where(where);
        }
        /// <summary>
        /// Get many by filter
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> @where)
        {
            return await Entities.Where(where).ToListAsync();
        }
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }
        /// <summary>
        /// Get all async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        #endregion

        /// <summary>
        /// Get entities
        /// </summary>
        public virtual IQueryable<T> Table => Entities;

        /// <summary>
        /// Get entities only readonly operations for performance
        /// </summary>
        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Is exist
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool IsExist(T entity)
        {
            return Entities.AsNoTracking().Any(e => e.Id.Equals(entity.Id));
        }
        /// <summary>
        /// Is exist async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsExistAsync(T entity)
        {
            return await Entities.AsNoTracking().AnyAsync(e => e.Id.Equals(entity.Id));
        }
        /// <summary>
        /// Is exist check by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExist(Guid id)
        {
            return Entities.AsNoTracking().Any(e => e.Id.Equals(id));
        }
        /// <summary>
        /// Is exist check by Id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Guid id)
        {
            return await Entities.AsNoTracking().AnyAsync(e => e.Id.Equals(id));
        }

        #endregion


    }
}
