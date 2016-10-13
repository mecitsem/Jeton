using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using Jeton.Core.Helpers;
using Jeton.Core.Common;
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
                throw new ArgumentNullException("app");

            return appRepository.Insert(app);
        }
        #endregion

        #region READ
        public App GetAppById(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException("appId");

            return appRepository.GetById(appId);
        }

        public App GetAppByName(string appName)
        {
            if (string.IsNullOrEmpty(appName))
                throw new ArgumentNullException("appName");

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
                throw new ArgumentNullException("app");

            appRepository.Update(app);
        }
        #endregion

        #region DELETE
        public void Delete(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException("appId");


            appRepository.Delete(a => a.AppID.Equals(appId));
        }
        #endregion


        public bool IsExist(Guid appId)
        {
            if (appId == null)
                throw new ArgumentNullException("appId");

            var table = appRepository.Table;

            return table.Any(a => a.AppID.Equals(appId));
        }

        public string GenerateAccessKey()
        {
            return CryptoManager.Encrypt(Guid.NewGuid().ToString(), ConfigHelper.GetPassPhrase());
        }
    }
}
