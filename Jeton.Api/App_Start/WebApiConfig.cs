using Jeton.Api.Initializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Jeton.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Initialization of Unity container
            Bootstrapper.Initialise(config);
            


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
