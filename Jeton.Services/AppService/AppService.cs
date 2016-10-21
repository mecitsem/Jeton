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
        private readonly IAppRepository appRepository;


        public AppService(IAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        #region CREATE
        public App Insert(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return appRepository.Insert(app);
        }
        #endregion

        #region READ
        public App GetAppById(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            return appRepository.GetById(appId);
        }

        public App GetAppByName(string appName)
        {
            if (string.IsNullOrEmpty(appName))
                throw new ArgumentNullException(nameof(appName));

            var table = appRepository.Table;

            return table.FirstOrDefault(a => a.Name.Equals(appName));
        }

        public IEnumerable<App> GetApps()
        {
            return appRepository.Table.ToList();
        }
        #endregion

        #region UPDATE
        public void Update(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            appRepository.Update(app);
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

            appRepository.Delete(app);
        }
        #endregion


        public bool IsExist(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));

            var table = appRepository.Table;

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

            bool result = false;

            if (!app.IsDeleted.HasValue)
                result = true;

            if (app.IsDeleted.HasValue && app.IsDeleted == true)
                result = true;

            return result;
        }
    }
}
