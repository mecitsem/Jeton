using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Helpers;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Services
{
    public class AppService : IAppService
    {

        #region Ctor
        private readonly IAppRepository _appRepository;

        public AppService(IAppRepository appRepository)
        {
            this._appRepository = appRepository;
        }
        #endregion

        #region CREATE
        /// <summary>
        /// Create app
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public App Create(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (_appRepository.IsExist(app))
                throw new ArgumentException("This app is already exist.");

            return _appRepository.Insert(app);
        }
        /// <summary>
        /// Create app async
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public async Task<App> CreateAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (_appRepository.IsExist(app))
                throw new ArgumentException("This app is already exist.");

            return await _appRepository.InsertAsync(app);
        }
        #endregion

        #region GET
        /// <summary>
        /// Get app by Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public App GetAppById(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            if (!_appRepository.IsExist(appId))
                throw new ArgumentException("This app is not exist.");

            return _appRepository.GetById(appId);
        }
        /// <summary>
        /// Get app by Id async
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<App> GetAppByIdAsync(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            if (!await _appRepository.IsExistAsync(appId))
                throw new ArgumentException("This app is not exist.");

            return await _appRepository.GetAppByIdAsync(appId);
        }
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
        /// <summary>
        /// Get all apps
        /// </summary>
        /// <returns></returns>
        public IEnumerable<App> GetApps()
        {
            return _appRepository.GetAllApps();
        }
        /// <summary>
        /// Get all apps async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<App>> GetAppsAsync()
        {
            return await _appRepository.GetAllAppsAsync();
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// Update app
        /// </summary>
        /// <param name="app"></param>
        public void Update(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (!_appRepository.IsExist(app))
                throw new ArgumentException("This app is not exist.");

            _appRepository.Update(app);
        }
        /// <summary>
        /// Update app async
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public async Task UpdateAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));


            if (!await _appRepository.IsExistAsync(app))
                throw new ArgumentException("This app is not exist.");

            await _appRepository.UpdateAsync(app);
        }

        #endregion

        #region DELETE
        /// <summary>
        /// Delete app by Id
        /// </summary>
        /// <param name="appId"></param>
        public void Delete(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            if (!IsExist(appId))
                throw new ArgumentException("App is not exist");

            var app = _appRepository.GetAppById(appId);

            _appRepository.Delete(app);
        }
        /// <summary>
        /// Delete app by Id async
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            if (!IsExist(appId))
                throw new ArgumentException("App is not exist");

            var app = await _appRepository.GetAppByIdAsync(appId);

            await _appRepository.DeleteAsync(app);
        }


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
        /// Check app isexist by Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public bool IsExist(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            return _appRepository.IsExist(appId);
        }
        /// <summary>
        /// Check app isexist by Id async
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            return await _appRepository.IsExistAsync(appId);
        }

        /// <summary>
        /// Generate accesskey
        /// </summary>
        /// <returns></returns>
        public string GenerateAccessKey()
        {
            var passPhrase = ConfigHelper.GetPassPhrase();

            if (passPhrase == null)
                throw new ArgumentException("PassPhrase is null. Please configure passphrase");

            return CryptoHelper.Encrypt(Guid.NewGuid().ToString(), passPhrase);
        }

    }
}
