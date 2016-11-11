using Jeton.Core.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Jeton.Api.Extensions;
using Jeton.Core.Helpers;

namespace Jeton.Api.Handlers
{
    public class LoggerDelegatingHandler : DelegatingHandler
    {
        private static readonly Logger logger = LogManager.GetLogger("RequestTracer");

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();

            var guid = Guid.NewGuid();
            MappedDiagnosticsContext.Set("requestid", guid.ToString());

            stopwatch.Start();
            //Request Url
            logger.Trace($"Requesting:[{request.Method.Method}] Url : {request.RequestUri}");

            //Access Key
            if (request.HeaderKeyIsExist(Constants.AccessKey))
                logger.Trace($"Access Key : { request.GetHeaderValue(Constants.AccessKey) }");

            var response = await base.SendAsync(request, cancellationToken);

            //Response Detail
            if (response != null)
                logger.Trace($"Response: { JsonHelper.Serialize(await response.Content.ReadAsStringAsync())}");

            stopwatch.Stop();

            logger.Trace($"Request completed. Duration:{stopwatch.Elapsed} StatusCode:{(int)response.StatusCode} Status: {response.StatusCode}");

            return response;
        }
    }
}