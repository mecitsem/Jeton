using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data
{
    public class RepositoryBase<T> where T : class
    {
        #region Properties
        private JetonEntities dataContext;
        private readonly IDbSet<T> dbSet;
        
        protected IDbFactory DbFactory {
            get;
            private set;
        }

        protected JetonEntities DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementaion
        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        /// <summary>
        /// DELETE MANY
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
        {
            return dbSet.Find(id);
        }
        /// <summary>
        /// GET ALL
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        /// <summary>
        /// GET MANY
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }
        #endregion
    }
}
