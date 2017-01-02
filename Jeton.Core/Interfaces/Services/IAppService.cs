using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface IAppService
    {
        bool IsActive(App app);
        string GenerateAccessKey();
        bool IsExist(Guid appId);
        IEnumerable<App> GetApps();
        App GetAppById(Guid appId);
        App GetAppByName(string appName);
        App Insert(App app);
        void Update(App app);
        void Delete(Guid appId);

        Task<bool> IsActiveAsync(App app);
        Task<string> GenerateAccessKeyAsync();
        Task<bool> IsExistAsync(Guid appId);
        Task<IEnumerable<App>> GetAppsAsync();
        Task<App> GetAppByIdAsync(Guid appId);
        Task<App> GetAppByNameAsync(string appName);
        Task<App> InsertAsync(App app);
        Task UpdateAsync(App app);
        Task DeleteAsync(Guid appId);
    }
}
