using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface IAppService
    {

        //GetAppByName
        App GetAppByName(string appName);
        Task<App> GetAppByNameAsync(string appName);

        //IsActive
        bool IsActive(App app);
        Task<bool> IsActiveAsync(App app);

        //Generate AccessKey
        string GenerateAccessKey();

    }
}
