using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Repositories.AppRepo
{
    public interface IAppRepository : IRepository<App>
    {
        App GetAppByName(string appName);
        App GetAppById(Guid appId);
        bool IsExist(Guid appId);
    }
}
