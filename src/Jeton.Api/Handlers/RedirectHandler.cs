using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Jeton.Api.Handlers
{
    public class RedirectHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, string> _rootUrlResolver;
        private readonly string _redirectPath;

        public RedirectHandler(Func<HttpRequestMessage, string> rootUrlResolver, string redirectPath)
        {
            _rootUrlResolver = rootUrlResolver;
            _redirectPath = redirectPath;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var redirectUrl = _rootUrlResolver(request) + "/" + _redirectPath;

            var response = request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(redirectUrl);

            var tcs = new TaskCompletionSource<HttpResponseMessage>();

            tcs.SetResult(response);

            return tcs.Task;
        }
    }
}