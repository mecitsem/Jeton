using Jeton.Api.Handlers;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Jeton.Api.App_Start
{
    public class LoggingConfig
    {
        public static void Init(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new LoggerDelegatingHandler());
        }
    }
}