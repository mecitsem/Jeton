using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Services;
using Jeton.Services;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;

namespace Jeton.Api.Handlers
{
    public class LogHandler : DelegatingHandler
    {
        private readonly ILogService _logService;

        public LogHandler(ILogService logService)
        {
            _logService = logService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiLog = CreateApiLog(request);

            if (request.Content != null && apiLog != null)
            {
                await request.Content.ReadAsStringAsync().ContinueWith(task =>
                {
                    apiLog.RequestContentBody = task.Result;
                }, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
           {

               var response = task.Result;

               if (apiLog == null) return response;

               try
               {
                   apiLog.ResponseStatusCode = (int)response.StatusCode;
                   apiLog.ResponseTimestamp = Core.Common.Constants.Now;
                   apiLog.ResponseContentBody = response?.Content?.ReadAsStringAsync()?.Result;
                   apiLog.ResponseContentType = response?.Content?.Headers?.ContentType?.MediaType;
                   apiLog.ResponseHeaders = SerializeHeaders(response?.Content?.Headers);

                   //TODO:Log
                   _logService.CreateAsync(apiLog);
               }
               catch
               {
                   // ignored
               }


               return response;
           }, cancellationToken);
        }

        private static Log CreateApiLog(HttpRequestMessage request)
        {
            Log log = null;
            try
            {
                var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);
                var routeData = request.GetRouteData();

                log = new Log()
                {
                    Created = Core.Common.Constants.Now,
                    Application = Core.Common.Constants.Application,
                    Machine = Environment.MachineName,
                    RequestContentType = context?.Request?.ContentType,
                    RequestRouteTemplate = routeData?.Route.RouteTemplate,
                    RequestRouteData = SerializeRouteData(routeData),
                    RequestIpAddress = context?.Request?.UserHostAddress,
                    RequestMethod = request?.Method?.Method,
                    RequestHeaders = SerializeHeaders(request?.Headers),
                    RequestTimestamp = Core.Common.Constants.Now,
                    RequestUri = request?.RequestUri?.ToString()

                };
            }
            catch
            {
                // ignored
            }

            return log;
        }

        private static string SerializeRouteData(IHttpRouteData routeData)
        {
            if (routeData == null) return string.Empty;

            try
            {
                return JsonConvert.SerializeObject(routeData);
            }
            catch
            {
                // ignored
                return string.Empty;
            }
        }

        private static string SerializeHeaders(HttpHeaders headers)
        {
            if (headers == null) return string.Empty;
            try
            {
                var dict = new Dictionary<string, string>();
                foreach (var item in headers.ToList())
                {
                    if (item.Value == null) continue;

                    var headerVal = item.Value.Aggregate(string.Empty, (current, v) => current + (v + " "));

                    headerVal = headerVal.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, headerVal);
                }

                return JsonConvert.SerializeObject(dict);
            }
            catch
            {

                return string.Empty;
            }

        }
    }



}