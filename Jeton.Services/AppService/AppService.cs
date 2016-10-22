using System;
using System.Collections.Generic;
using System.Linq;
using Jeton.Core.Entities;
using Jeton.Core.Helpers;
using Jeton.Data.Repositories.AppRepo;

namespace Jeton.Services.AppService
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


        public bool IsExist(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            var table = _appRepository.Table;

            return table.Any(a => a.AppID.Equals(appId));
        }

        public string GenerateAccessKey()
        {
            return CryptoHelper.Encrypt(Guid.NewGuid().ToString(), ConfigHelper.GetPassPhrase());
        }

        public bool IsActive(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            var result = !app.IsDeleted.HasValue || app.IsDeleted == true;

            return result;
        }
    }
}
