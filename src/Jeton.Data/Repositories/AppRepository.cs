﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories
{
    public class AppRepository : BaseRepository<App>, IAppRepository
    {
        public AppRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        #region INSERT
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override App Insert(App entity)
        {
            if (!IsExistByName(entity.Name)) return base.Insert(entity);
            var index = 0;
            var name = entity.Name;
            while (IsExistByName(entity.Name))
            {
                entity.Name = $"{name}_{++index}";
            }
            return base.Insert(entity);
        }
        /// <summary>
        /// Insert entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<App> InsertAsync(App entity)
        {
            if (!IsExistByName(entity.Name)) return await base.InsertAsync(entity);
            var index = 0;
            var name = entity.Name;
            while (await IsExistByNameAsync(entity.Name))
            {
                entity.Name = $"{name}_{++index}";
            }
            return await base.InsertAsync(entity);
        }

        #endregion

        #region UPDATE
        #endregion

        #region DELETE
        #endregion

        #region SELECT
        /// <summary>
        /// Get app by Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public App GetAppById(Guid appId)
        {
            return base.GetById(appId);
        }
        /// <summary>
        /// Get app by Id async
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<App> GetAppByIdAsync(Guid appId)
        {
            return await GetByIdAsync(appId);
        }
        /// <summary>
        /// Get app by name
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public App GetAppByName(string appName)
        {
            return Table.AsEnumerable().FirstOrDefault(a => a.Name.Equals(appName));
        }
        /// <summary>
        /// Get app by name async
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public async Task<App> GetAppByNameAsync(string appName)
        {
            return await Table.FirstOrDefaultAsync(a => string.Equals(a.Name, appName));
        }
        /// <summary>
        /// Get all apps
        /// </summary>
        /// <returns></returns>
        public IEnumerable<App> GetAllApps()
        {
            return Table.ToList();
        }
        /// <summary>
        /// Get all apps async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<App>> GetAllAppsAsync()
        {
            return await Table.ToListAsync();
        }
        #endregion


        /// <summary>
        /// Is exist check by name
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public bool IsExistByName(string appName)
        {
            return TableNoTracking.Any(a => a.Name.Equals(appName));
        }
        /// <summary>
        /// Is exist check by name async
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public async Task<bool> IsExistByNameAsync(string appName)
        {
            return await TableNoTracking.AnyAsync(a => a.Name.Equals(appName));
        }

       
    }
}
