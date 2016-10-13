﻿using Jeton.Core.Entities;
using Jeton.Data;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;
using Jeton.Data.Repositories.AppRepo;
using Jeton.Data.Repositories.TokenRepo;
using Jeton.Data.Repositories.UserRepo;
using Jeton.Services.AppService;
using Jeton.Services.TokenService;
using Jeton.Services.UserService;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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


            container.RegisterType<IDbFactory, DbFactory>(new HierarchicalLifetimeManager());

            //Repository
            container.RegisterType<IAppRepository, AppRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITokenRepository, TokenRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());

            //Services
            container.RegisterType<IAppService, AppService>();
            container.RegisterType<ITokenService, TokenService>();
            container.RegisterType<IUserService, UserService>();



            config.DependencyResolver = new UnityResolver(container);

        }
    }
}