using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface IAppRepository : IRepository<App>
    {
        //Get app by name
        App GetAppByName(string appName);
        Task<App> GetAppByNameAsync(string appName);

        //Get app by Id
        App GetAppById(Guid appId);
        Task<App> GetAppByIdAsync(Guid appId);

        //Check isExist by appId
        bool IsExist(Guid appId);
        Task<bool> IsExistAsync(Guid appId);

        //Check isExist by entity
        bool IsExist(App app);
        Task<bool> IsExistAsync(App app);

        //Check isExist by name
        bool IsExistByName(string appName);
        Task<bool> IsExistByNameAsync(string appName);

        //Get all apps
        IEnumerable<App> GetAllApps();
        Task<IEnumerable<App>> GetAllAppsAsync();
    }
}
