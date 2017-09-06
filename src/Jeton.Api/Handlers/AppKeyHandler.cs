using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Jeton.Api.Extensions;
using Jeton.Core.Common;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Api.Handlers
{
    public class AppKeyHandler : DelegatingHandler
    {
        private readonly IAppService _appService;

        public AppKeyHandler(IAppService appService)
        {
            _appService = appService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            #region CHECK Parameters

            var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);

            var isTokenRequest = context.Request.Url.AbsolutePath.StartsWith("api/token");

            var appId = isTokenRequest ? context?.Request?.Url?.Segments.LastOrDefault() : string.Empty;

            #region AppSecurity
            if (isTokenRequest)
            {
                Guid _appId;
                //Check AppId
                if (string.IsNullOrEmpty(appId) || !Guid.TryParse(appId, out _appId) || _appId.Equals(default(Guid)))
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "AppId is null or not Guid format");
                    tsc.SetResult(response);
                    return await tsc.Task;
                }

                //Request Header

                //Check AccessKey => in swagger apiKey
                if (!request.HeaderKeyIsExist(Constants.ApiKey))
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "apiKey is required. Please add your header.");
                    tsc.SetResult(response);
                    return await tsc.Task;
                }


                var apiKey = request.GetHeaderValue(Constants.ApiKey);

                //Check AccessKey Value
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "apiKey is null or empty. Please add your AccessKey.");
                    tsc.SetResult(response);
                    return await tsc.Task;
                }


                #region Check App

                //Check App is Exist
                if (!_appService.IsExistAsync(_appId).Result)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "AppId is invalid. Please register your app");
                    tsc.SetResult(response);
                    return await tsc.Task;
                }


                //Get APP
                var app = _appService.GetByIdAsync(_appId).Result;

                //Check app is active
                if (!_appService.IsActiveAsync(app).Result)
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "The app is not found");
                    tsc.SetResult(response);
                    return await tsc.Task;
                }

                //Check Access Key
                if (!app.AccessKey.Equals(apiKey))
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                    tsc.SetResult(response);
                    return await tsc.Task;
                }


                #endregion


            }
            #endregion

            return response;
           
            #endregion
        }
    }
}