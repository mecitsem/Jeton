using Jeton.Core.Common;
using Jeton.Services.AppService;
using Jeton.Services.TokenService;
using Jeton.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jeton.Api.Controllers
{
    public class TokenController : ApiController
    {

        private readonly ITokenService tokenService;
        private readonly IAppService appService;
        private readonly IUserService userService;

        public TokenController(ITokenService tokenService, IAppService appService, IUserService userService)
        {
            this.tokenService = tokenService;
            this.appService = appService;
            this.userService = userService;
        }

        [Route("~/api/{appId}/{userName}/{userNameId}")]
        [HttpGet]
        public HttpResponseMessage GenerateToken(string appId, string userName, string userNameId)
        {
            HttpResponseMessage response;
            try
            {
                Guid _appId = new Guid();

                //Check AppId
                if (string.IsNullOrEmpty(appId) && !Guid.TryParse(appId, out _appId))
                {
                    throw new ArgumentNullException("appKey is null!");
                }
                //Check UserName
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException("userName is null!");
                }
                //Check UserNameId
                if (string.IsNullOrEmpty(userNameId))
                {
                    throw new ArgumentNullException("userNameId is null!");
                }

                //Check App 
                var headerValues = Request.Headers.GetValues(Constants.AccessKey);

                if (headerValues == null)
                    throw new ArgumentNullException("AccessKey parameter is not exist!");

                var accessKey = headerValues.FirstOrDefault();

                if (string.IsNullOrEmpty(accessKey))
                    throw new ArgumentNullException("AccessKey is null or empty");

                //Get App
                var app = appService.GetAppById(_appId);

                if (app == null)
                    throw new ArgumentNullException("App is not registered or invalid. Please contact your administor.");

                if (!app.IsRoot)
                    throw new ArgumentException("This app can not generate token becase this app is not root app.");

                //Check Access Key
                if (!app.AccessKey.Equals(accessKey))
                    throw new ArgumentException("This access key is invalid.");

                //Check User
                var user = userService.GetUserByNameId(userNameId);

                if (user == null)
                {
                    userService.Insert(new Core.Entities.User()
                    {
                        Name = userName,
                        NameId = userNameId
                    });
                }


                response = Request.CreateResponse(HttpStatusCode.OK, "");
            }
            catch (Exception ex)
            {

                response = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }

            return response;
        }


    }
}
