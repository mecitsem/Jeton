using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Data.Interfaces;
using Jeton.Data.Entities;
using Jeton.Data.Data;

namespace Jeton.Data.Repositories
{
    public class RegisteredAppRepository : IRegisteredAppRepository
    {
        private JetonDBContext _context;

        public RegisteredAppRepository()
        {
            if (_context == null) _context = new JetonDBContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredApp"></param>
        /// <returns></returns>
        public virtual RegisteredApp Add(RegisteredApp registeredApp)
        {
            RegisteredApp result = null;
            if (!_context.RegisteredApps.Any(r => r.Equals(registeredApp)))
            {
                result = _context.RegisteredApps.Add(registeredApp);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            var registeredApp = _context.RegisteredApps.FirstOrDefault(r => r.Id.Equals(id));
            _context.RegisteredApps.Remove(registeredApp);
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessKey"></param>
        /// <returns></returns>
        public virtual RegisteredApp GetRegistedAppByAccessKey(string accessKey)
        {
            return _context.RegisteredApps.FirstOrDefault(r => r.AccessKey.Equals(accessKey));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual RegisteredApp GetRegistedAppById(int id)
        {
            return _context.RegisteredApps.FirstOrDefault(r => r.Id.Equals(id));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public virtual RegisteredApp GetRegistedAppByName(string appName)
        {
            return _context.RegisteredApps.FirstOrDefault(r => r.AppName.ToLowerInvariant().Equals(appName.ToLowerInvariant()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<RegisteredApp> GetRegisteredApps()
        {
            return _context.RegisteredApps.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="registeredApp"></param>
        /// <returns></returns>
        public virtual RegisteredApp Update(int id, RegisteredApp registeredApp)
        {
            RegisteredApp result = null;
            if (_context.RegisteredApps.Any(r => r.Id.Equals(id)))
            {
                _context.Entry(registeredApp).State = System.Data.Entity.EntityState.Modified;
                result = registeredApp;
            }
            return result;
        }
    }
}
