using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeton.Core.Interfaces;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.DbContext;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Infrastructure
{
    public partial class RepositoryBase<T> : IRepository<T> where T : class, IEntity
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

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }


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

        #region Implementaion

        public Task InsertAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

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

                Entities.Attach(entity);

                DbContext.Entry(entity).State = EntityState.Modified;

                DbContext.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Attach(entity);

                DbContext.Entry(entity).State = EntityState.Modified;

                await DbContext.CommitAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// DELETE
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

        public async Task DeleteAsync(T entity)
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

        public async Task DeleteAsync(Expression<Func<T, bool>> @where)
        {
            var objects = Entities.Where<T>(where).AsEnumerable();
            foreach (var obj in objects)
                await DeleteAsync(obj);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
        {
            return Entities.Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
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
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var result = Entities.Add(entity);

                DbContext.Commit();

                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

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
                    Entities.Add(entity);

                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public IQueryable<T> Table => Entities;

        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        #endregion


    }
}
