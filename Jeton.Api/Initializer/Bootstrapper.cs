using System.CodeDom;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Jeton.Api.Handlers;
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
            container.RegisterType<IAppRepository, AppRepository>();
            container.RegisterType<ITokenRepository, TokenRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ISettingRepository, SettingRepository>();
            container.RegisterType<ILogRepository, LogRepository>();

            //Services
            container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            container.RegisterType<IAppService, AppService>();
            container.RegisterType<ITokenService, TokenService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ISettingService, SettingService>();
            container.RegisterType<ILogService, LogService>();


            //DelegationHandlers
            GlobalConfiguration.Configuration.MessageHandlers.Add(new LogHandler(container.Resolve<ILogService>()));

            config.DependencyResolver = new UnityResolver(container);

        }
    }
}