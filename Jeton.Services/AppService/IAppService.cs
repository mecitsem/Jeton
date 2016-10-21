using Jeton.Core.Entities;
using System;
using System.Collections.Generic;

namespace Jeton.Services.AppService
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
    }
}
