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
using Jeton.Api.Extensions;
using Jeton.Api.Models;
using Jeton.Api.DTOs;

namespace Jeton.Api.Controllers
{
    public class TokenController : ApiController
    {

        private readonly ITokenService tokenService;
        private readonly IAppService appService;
        private readonly IUserService userService;

        public TokenController(IAppService appService, ITokenService tokenService, IUserService userService)
        {
            this.appService = appService;
            this.tokenService = tokenService;
            this.userService = userService;
        }

        

        [Route("api/token/{appId}")]
        [HttpPost]
        public IHttpActionResult GenerateToken([FromUri] string appId, UserModel userModel)
        {
            IHttpActionResult response;
            try
            {
                Guid _appId = new Guid();

                string accessKey;

                #region CHECK Parameters

                //Check AppId
                if (string.IsNullOrEmpty(appId) || !Guid.TryParse(appId, out _appId) || _appId.Equals(default(Guid)))
                    return BadRequest("AppId is null or not Guid format");


                //Request Header


                //Check AccessKey
                if (!Request.HeaderKeyIsExist(Constants.AccessKey))
                    return BadRequest("AccessKey is required. Please add your header.");

                accessKey = Request.GetHeaderValue(Constants.AccessKey);

                //Check AccessKey Value
                if (string.IsNullOrWhiteSpace(accessKey))
                    return BadRequest("AccessKey is null or empty. Please add your AccessKey.");


                if(!ModelState.IsValid)
                    return BadRequest("UserName is required. Please add your header.");




                #endregion

                #region Check App

                //Check App is Exist
                if (!appService.IsExist(_appId))
                    return BadRequest("AppId is invalid. Please register your app");


                //Get APP
                var app = appService.GetAppById(_appId);

                //Check Access Key
                if (!app.AccessKey.Equals(accessKey))
                    return Unauthorized();

                if (!app.IsRoot)
                    return BadRequest("This app can not generate token because this app is not a root app.");

                #endregion

                #region Check User

                //Get User
                Core.Entities.User user = userService.GetUserByNameId(userModel.NameId);

                if (user == null)
                {
                    user = userService.Insert(new Core.Entities.User()
                    {
                        Name = userModel.Name,
                        NameId = userModel.NameId,
                    });
                }
                #endregion

                //Generate Token
                var token = tokenService.Generate(user);
                var tokenDTO = new TokenDTO() {
                    TokenKey = token.TokenKey
                };
                //Response
                response = Ok(tokenDTO);
            }
            catch (Exception ex)
            {
                response = InternalServerError(ex);
            }

            return response;
        }


    }
}
