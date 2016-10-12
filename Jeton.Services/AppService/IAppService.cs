using Jeton.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Services.AppService
{
    public interface IAppService
    {
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
