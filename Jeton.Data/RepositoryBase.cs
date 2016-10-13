using Jeton.Core;
using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data
{
    public partial class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private JetonEntities dataContext;
        private IDbSet<T> _entities;
    

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                return _entities ?? (_entities = DbContext.Set<T>());
            }
        }

        protected JetonEntities DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }

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
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
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
                    throw new ArgumentNullException("entity");

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
                    throw new ArgumentNullException("entity");

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
            IEnumerable<T> objects = this.Entities.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
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
                    throw new ArgumentNullException("entity");

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
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Add(entity);

                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }
        #endregion


    }
}
