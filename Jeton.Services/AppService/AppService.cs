using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Repositories.AppRepo;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Services.AppService
{
    public class AppService : IAppService
    {
        private readonly IAppRepository appRepository;
        private readonly IUnitOfWork unitOfWork;

        public AppService(IAppRepository appRepository, IUnitOfWork unitOfWork)
        {
            this.appRepository = appRepository;
            this.unitOfWork = unitOfWork;
        }

        #region CREATE
        public void Insert(App app)
        {
            appRepository.Add(app);
        }
        #endregion

        #region READ
        public App GetAppById(Guid appId)
        {
            return appRepository.GetAppById(appId);
        }

        public App GetAppByName(string appName)
        {
            return appRepository.GetAppByName(appName);
        }

        public IEnumerable<App> GetApps()
        {
            return appRepository.GetAll();
        }
        #endregion

        #region UPDATE
        public void Update(App app)
        {
            appRepository.Update(app);
        }
        #endregion

        #region DELETE
        public void Delete(Guid appId)
        {
            var app = appRepository.GetAppById(appId);
            appRepository.Delete(app);
        }
        #endregion


        public void Save()
        {
            unitOfWork.Commit();
        }

       
    }
}
