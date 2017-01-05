using System.CodeDom;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;
using Jeton.Data.Repositories;
using Jeton.Services;

namespace Jeton.Api.Initializer
{
    /// <summary>
    /// Register all repositories for Dependency Injection IoC by Unity Container
    /// </summary>
    public class Bootstrapper
    {
        public static void Initialise(HttpConfiguration config)
        {
            var container = new UnityContainer();

            //Factory
            container.RegisterType<IDbFactory, DbFactory>(new HierarchicalLifetimeManager());

            //Repository
            container.RegisterType(typeof(IRepository<>), typeof(BaseRepository<>));
            container.RegisterType<IAppRepository, AppRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITokenRepository, TokenRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISettingRepository, SettingRepository>(new HierarchicalLifetimeManager());

            //Services
            container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            container.RegisterType<IAppService, AppService>();
            container.RegisterType<ITokenService, TokenService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ISettingService, SettingService>();


            config.DependencyResolver = new UnityResolver(container);

        }
    }
}