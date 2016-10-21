using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Jeton.Data
{
    public partial class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private JetonEntities _dataContext;
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

        protected JetonEntities DbContext => _dataContext ?? (_dataContext = DbFactory.Init());

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

                this.DbContext.SaveChanges();
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
                    T _entity = entity;

                    _entity.GetType().GetProperty("IsDeleted").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    var dbEntityEntry = this.DbContext.Entry(entity);

                    if (dbEntityEntry.State != EntityState.Deleted)
                    {
                        dbEntityEntry.State = EntityState.Deleted;
                    }
                    else
                    {
                        this.Entities.Attach(entity);
                        this.Entities.Remove(entity);
                    }

                    this.DbContext.SaveChanges();
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
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// GET MANY
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this.Entities.Where(where);
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

                var result = this.Entities.Add(entity);

                this.DbContext.SaveChanges();

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
                    this.Entities.Add(entity);

                this.DbContext.SaveChanges();
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
