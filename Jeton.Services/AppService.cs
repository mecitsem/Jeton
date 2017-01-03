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
        private readonly IAppRepository _appRepository;


        public AppService(IAppRepository appRepository)
        {
            this._appRepository = appRepository;
        }

        #region CREATE
        public App Insert(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return _appRepository.Insert(app);
        }
        #endregion

        #region READ
        public App GetAppById(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            return _appRepository.GetById(appId);
        }

        public App GetAppByName(string appName)
        {
            if (string.IsNullOrEmpty(appName))
                throw new ArgumentNullException(nameof(appName));

            var table = _appRepository.Table;

            return table.FirstOrDefault(a => a.Name.Equals(appName));
        }

        public IEnumerable<App> GetApps()
        {
            return _appRepository.Table.ToList();
        }
        #endregion

        #region UPDATE
        public void Update(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            _appRepository.Update(app);
        }
        #endregion

        #region DELETE
        public void Delete(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            if (!IsExist(appId))
                throw new ArgumentException("App is not exist");

            var app = GetAppById(appId);

            _appRepository.Delete(app);
        }



        #endregion

        public async Task<bool> IsActiveAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (app.Id == default(Guid) || !_appRepository.IsExist(app))
                throw new ArgumentException("This is not exist app");

            var existedApp = await GetAppByIdAsync(app.Id);

            var result = !(existedApp.IsDeleted.HasValue && existedApp.IsDeleted == true);

            return result;
        }

        public async Task<bool> IsExistAsync(Guid appId)
        {
            return await _appRepository.IsExistAsync(appId);
        }

        public async Task<IEnumerable<App>> GetAppsAsync()
        {
            return await _appRepository.GetAllAppsAsync();
        }

        public async Task<App> GetAppByIdAsync(Guid appId)
        {
            return await _appRepository.GetAppByIdAsync(appId);
        }

        public async Task<App> GetAppByNameAsync(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                throw new ArgumentNullException(nameof(appName));

            return await _appRepository.GetAppByNameAsync(appName);
        }

        public async Task<App> InsertAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return await _appRepository.InsertAsync(app);
        }

        public async Task UpdateAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            await _appRepository.UpdateAsync(app);
        }

        public async Task DeleteAsync(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            if (!IsExist(appId))
                throw new ArgumentException("App is not exist");

            var app = GetAppById(appId);

            await _appRepository.DeleteAsync(app);
        }
        public bool IsExist(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            var table = _appRepository.Table;
            
            return table.Any(a => a.Id.Equals(appId));
        }

        public string GenerateAccessKey()
        {
            var passPhrase = ConfigHelper.GetPassPhrase();

            if (passPhrase == null)
                throw new ArgumentException("PassPhrase is null. Please configure passphrase");

            return CryptoHelper.Encrypt(Guid.NewGuid().ToString(), passPhrase);
        }
        public bool IsActive(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (app.Id == default(Guid) || !_appRepository.IsExist(app))
                throw new ArgumentException("This is not exist app");

            var existedApp = GetAppById(app.Id);

            var result = !(existedApp.IsDeleted.HasValue && existedApp.IsDeleted == true);

            return result;
        }
    }
}
