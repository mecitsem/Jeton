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

        [Route("~/api/{appId}")]
        [HttpPost]
        public IHttpActionResult GenerateToken([FromUri] string appId)
        {
            IHttpActionResult response;
            try
            {
                Guid _appId = new Guid();
                IEnumerable<string> accessKeys, userNames, userNameIds;
                string accessKey, userName, userNameId;

                #region CHECK Parameters

                //Check AppId
                if (string.IsNullOrEmpty(appId) && !Guid.TryParse(appId, out _appId))
                    return BadRequest("AppId is null or not Guid format");


                //Request Header
                var headers = Request.Headers;

                //Check AccessKey
                if (!headers.Contains(Constants.AccessKey))
                    return BadRequest("AccessKey is required. Please add your header.");


                //Get AccessKey
                if (!headers.TryGetValues(Constants.AccessKey, out accessKeys))
                    return BadRequest("AccessKey is null. Please add your AccessKey.");

                // SET AccessKEY
                accessKey = accessKeys.FirstOrDefault();

                //Check UserName
                if (!headers.Contains(Constants.UserName))
                    return BadRequest("UserName is required. Please add your header.");

                //Get UserName
                if (!headers.TryGetValues(Constants.UserName, out userNames))
                    return BadRequest("UserName is null. Please add your UserName.");

                userName = userNames.FirstOrDefault();

                //Check UserNameId
                if (!headers.Contains(Constants.UserNameId))
                    return BadRequest("UserNameId is required. Please add your header.");


                //Get UserNameId
                if (!headers.TryGetValues(Constants.UserNameId, out userNameIds))
                    return BadRequest("UserNameId is null. Please add your UserNameId.");

                userNameId = userNameIds.FirstOrDefault();

                #endregion

                #region Check App

                //Check App is Exist
                if (!appService.IsExist(_appId))
                    return BadRequest("AppId is invalid. Please register your App");


                //Get APP
                var app = appService.GetAppById(_appId);

                //Check Access Key
                if (!app.AccessKey.Equals(accessKey))
                    return Unauthorized();

                if (!app.IsRoot)
                    return BadRequest("This app can not generate token becase this app is not root app.");

                #endregion

                #region Check User
                
                //Get User
                Core.Entities.User user = userService.GetUserByNameId(userNameId);

                if (user == null)
                {
                    user = userService.Insert(new Core.Entities.User()
                    {
                        Name = userName,
                        NameId = userNameId,
                    });
                }
                #endregion

                //Generate Token
                var token = tokenService.Generate(user);

                //Response
                response =  Ok(token.TokenKey);
            }
            catch (Exception ex)
            {
                response = InternalServerError(ex);
            }

            return response;
        }


    }
}
