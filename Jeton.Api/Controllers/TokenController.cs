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

        [Route("~/api/{appKey}/{userName}/{userNameId}")]
        [HttpGet]
        public HttpResponseMessage GenerateToken(string appKey, string userName, string userNameId)
        {
            HttpResponseMessage response;
            try
            {
                if (string.IsNullOrEmpty(appKey))
                {
                    throw new ArgumentNullException("appKey is null!");
                }

                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException("userName is null!");
                }

                if (string.IsNullOrEmpty(userNameId))
                {
                    throw new ArgumentNullException("userNameId is null!");
                }

                var token = tokenService.


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
