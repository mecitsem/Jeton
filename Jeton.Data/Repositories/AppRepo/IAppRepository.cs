using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using System;

namespace Jeton.Data.Repositories.AppRepo
{
    public interface IAppRepository : IRepository<App>
    {
        App GetAppByName(string appName);
        App GetAppById(Guid appId);
        bool IsExist(Guid appId);
        bool IsExist(App app);
        bool IsExistByName(string appName);
    }
}
