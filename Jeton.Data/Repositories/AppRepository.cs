using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories
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
            return DbContext.Apps.FirstOrDefault(a => a.Name.Equals(appName, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsExist(Guid appId)
        {
            return DbContext.Apps.Any(a => a.Id.Equals(appId));
        }

        public bool IsExist(App app)
        {
            return DbContext.Apps.Any(a => a.Id.Equals(app.Id) && a.Name.Equals(app.Name, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsExistByName(string appName)
        {
            return DbContext.Apps.Any(a => a.Name.Equals(appName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<App> GetAppByNameAsync(string appName)
        {
            return await DbContext.Apps.FirstOrDefaultAsync(a => string.Equals(a.Name, appName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<App> GetAppByIdAsync(Guid appId)
        {
            return await DbContext.Apps.FindAsync(appId);
        }

        public async Task<bool> IsExistAsync(Guid appId)
        {
            return await DbContext.Apps.AnyAsync(a => a.Id.Equals(appId));
        }

        public async Task<bool> IsExistAsync(App app)
        {
            return await DbContext.Apps.AnyAsync(a => a.Id.Equals(app.Id) && a.Name.Equals(app.Name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> IsExistByNameAsync(string appName)
        {
            return await DbContext.Apps.AnyAsync(a => a.Name.Equals(appName, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsExistByName(App app)
        {
            return DbContext.Apps.Any(a => !a.Id.Equals(app.Id) && a.Name.Equals(app.Name, StringComparison.OrdinalIgnoreCase));
        }

        public override void Update(App entity)
        {
            entity.Modified = DateTime.Now;
            base.Update(entity);
        }

        public override App Insert(App entity)
        {
            if (!IsExistByName(entity)) return base.Insert(entity);
            var index = 0;
            var name = entity.Name;
            while (IsExistByName(entity))
            {
                entity.Name = $"{name}_{++index}";
            }
            return base.Insert(entity);
        }


    }
}
