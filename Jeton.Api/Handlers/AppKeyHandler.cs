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

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
          
            #region CHECK Parameters

            var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);

            var appId = context?.Request?.Url?.Segments.LastOrDefault();
        

            #endregion


            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;
                Guid _appId;
                //Check AppId
                if (string.IsNullOrEmpty(appId) || !Guid.TryParse(appId, out _appId) || _appId.Equals(default(Guid)))
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, "AppId is null or not Guid format");
                }

                //Request Header

                //Check AccessKey
                if (!request.HeaderKeyIsExist(Constants.AccessKey))
                    return request.CreateResponse(HttpStatusCode.BadRequest, "AccessKey is required. Please add your header.");

                var accessKey = request.GetHeaderValue(Constants.AccessKey);

                //Check AccessKey Value
                if (string.IsNullOrWhiteSpace(accessKey))
                    return request.CreateResponse(HttpStatusCode.BadRequest, "AccessKey is null or empty. Please add your AccessKey.");

                #region Check App

                //Check App is Exist
                if (!_appService.IsExistAsync(_appId).Result)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "AppId is invalid. Please register your app");

                //Get APP
                var app = _appService.GetByIdAsync(_appId).Result;

                //Check app is active
                if (!_appService.IsActiveAsync(app).Result)
                    return request.CreateResponse(HttpStatusCode.NotFound, "The app is not found");

                //Check Access Key
                if (!app.AccessKey.Equals(accessKey))
                    return request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");


                #endregion

                return response;

            }, cancellationToken);
        }
    }
}