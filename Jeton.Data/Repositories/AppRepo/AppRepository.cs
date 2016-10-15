using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Linq;

namespace Jeton.Data.Repositories.AppRepo
{
    public class AppRepository : RepositoryBase<App>, IAppRepository
    {
        public AppRepository(IDbFactory dbFactory) 
            : base(dbFactory) { }

        public App GetAppById(Guid appId)
        {
           return DbContext.Apps.Find(appId);
        }

        public App GetAppByName(string appName)
        {
            return DbContext.Apps.FirstOrDefault(a => a.Name.Equals(appName));
        }

        public bool IsExist(Guid appId)
        {
            return DbContext.Apps.Any(a => a.AppID.Equals(appId));
        }

        public override void Update(App entity)
        {
            entity.Modified = DateTime.Now;
            base.Update(entity);
        }
    }
}
