using System;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface IAppRepository : IRepository<App>
    {
        //Sync
        App GetAppByName(string appName);
        App GetAppById(Guid appId);
        bool IsExist(Guid appId);
        bool IsExist(App app);
        bool IsExistByName(string appName);
        //Async
        Task<App> GetAppByNameAsync(string appName);
        Task<App> GetAppByIdAsync(Guid appId);
        Task<bool> IsExistAsync(Guid appId);
        Task<bool> IsExistAsync(App app);
        Task<bool> IsExistByNameAsync(string appName);
    }
}
