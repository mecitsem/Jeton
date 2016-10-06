using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using Jeton.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Repositories.AppRepo
{
    public class AppRepository : RepositoryBase<App>, IAppRepository
    {
        public AppRepository(IDbFactory dbFactory) 
            : base(dbFactory) { }

        public App GetAppById(Guid appId)
        {
           return this.DbContext.Apps.Find(appId);
        }

        public App GetAppByName(string appName)
        {
            return this.DbContext.Apps.FirstOrDefault(a => a.Name.Equals(appName));
        }

        public override void Update(App entity)
        {
            entity.Modified = DateTime.Now;
            base.Update(entity);
        }
    }
}
