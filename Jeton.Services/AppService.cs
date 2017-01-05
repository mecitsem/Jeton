using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Common;
using Jeton.Core.Entities;
using Jeton.Core.Helpers;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;
using Jeton.Core.Managers;

namespace Jeton.Services
{
    public class AppService : BaseService<App>, IAppService
    {

        #region Ctor
        private readonly IAppRepository _appRepository;

        public AppService(IAppRepository appRepository) : base(appRepository)
        {
            this._appRepository = appRepository;
        }
        #endregion

        #region CREATE

        #endregion

        #region GET
        /// <summary>
        /// Get app by name
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public App GetAppByName(string appName)
        {
            if (string.IsNullOrEmpty(appName))
                throw new ArgumentNullException(nameof(appName));

            if (!_appRepository.IsExistByName(appName))
                throw new ArgumentException("This app is not exist.");

            return _appRepository.GetAppByName(appName);
        }
        /// <summary>
        /// Get app by name async
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public async Task<App> GetAppByNameAsync(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                throw new ArgumentNullException(nameof(appName));


            if (!await _appRepository.IsExistByNameAsync(appName))
                throw new ArgumentException("This app is not exist.");

            return await _appRepository.GetAppByNameAsync(appName);
        }
        #endregion

        #region UPDATE
        
        #endregion

        #region DELETE

        #endregion

        /// <summary>
        /// Check app is active
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public bool IsActive(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (app.Id == default(Guid) || !_appRepository.IsExist(app))
                throw new ArgumentException("This is not exist app");

            var existedApp = _appRepository.GetAppById(app.Id);
            
            var result = !(existedApp.IsDeleted.HasValue && existedApp.IsDeleted == true);

            return result;
        }
        /// <summary>
        /// Check app is active async
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public async Task<bool> IsActiveAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (app.Id == default(Guid) || !await _appRepository.IsExistAsync(app))
                throw new ArgumentException("App is not exist");

            var existedApp = await _appRepository.GetAppByIdAsync(app.Id);

            var result = !(existedApp.IsDeleted.HasValue && existedApp.IsDeleted == true);

            return result;
        }

        /// <summary>
        /// Generate accesskey
        /// </summary>
        /// <returns></returns>
        public string GenerateAccessKey()
        {
            return AccessKeyManager.Generate(Guid.NewGuid().ToString());
        }

    }
}
