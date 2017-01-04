using Jeton.Core.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Jeton.Api.Extensions;
using Jeton.Api.Models;
using Jeton.Api.DTOs;
using Jeton.Api.Filters;
using Jeton.Core.Interfaces.Services;
using NLog;

namespace Jeton.Api.Controllers
{
    public class TokenController : ApiController
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly ITokenService _tokenService;
        private readonly IAppService _appService;
        private readonly IUserService _userService;

        public TokenController(IAppService appService, ITokenService tokenService, IUserService userService)
        {
            _appService = appService;
            _tokenService = tokenService;
            _userService = userService;
        }




        [Route("api/token/generate/{appId}")]
        [ActionName("Generate")]
        [CheckModelForNull]
        [HttpPost]
        public IHttpActionResult GenerateToken([FromUri] string appId, [FromBody] UserModel userModel)
        {
            IHttpActionResult response;
            try
            {
                Guid _appId;
                #region CHECK Parameters

                //Check AppId
                if (string.IsNullOrEmpty(appId) || !Guid.TryParse(appId, out _appId) || _appId.Equals(default(Guid)))
                {
                 
                    return BadRequest("AppId is null or not Guid format");
                }


                //Request Header


                //Check AccessKey
                if (!Request.HeaderKeyIsExist(Constants.AccessKey))
                    return BadRequest("AccessKey is required. Please add your header.");

                var accessKey = Request.GetHeaderValue(Constants.AccessKey);

                //Check AccessKey Value
                if (string.IsNullOrWhiteSpace(accessKey))
                    return BadRequest("AccessKey is null or empty. Please add your AccessKey.");


                #endregion

                #region Check App

                //Check App is Exist
                if (!_appService.IsExist(_appId))
                    return BadRequest("AppId is invalid. Please register your app");



                //Get APP
                var app = _appService(_appId);

                //Check app is active
                if (!_appService.IsActive(app))
                    return NotFound();

                //Check Access Key
                if (!app.AccessKey.Equals(accessKey))
                    return Unauthorized();

                if (!app.IsRoot)
                    return BadRequest("This app can not generate token because this app is not a root app.");

                #endregion

                #region Check User

                //Get User
                var user = _userService.GetUserByNameId(userModel.UserNameId) ??
                                          _userService.Insert(new Core.Entities.User()
                                          {
                                              Name = userModel.UserName,
                                              NameId = userModel.UserNameId,
                                          });

                #endregion

                //Generate Token
                var token = _tokenService.Generate(user, app);
                var tokenDto = new TokenDTO()
                {
                    TokenKey = token.TokenKey
                };
                //Response
                response = Ok(tokenDto);
            }
            catch (Exception ex)
            {
                response = InternalServerError(ex);
            }

            return response;
        }

        [Route("api/token/check/{appId}")]
        [ActionName("Check")]
        [CheckModelForNull]
        [HttpPost]
        public IHttpActionResult CheckToken([FromUri] string appId, [FromBody] TokenModel tokenModel)
        {
            IHttpActionResult response;
            try
            {
                #region CHECK Parameters

                //Check AppId
                Guid _appId;
                if (string.IsNullOrEmpty(appId) || !Guid.TryParse(appId, out _appId) || _appId.Equals(default(Guid)))
                    return BadRequest("AppId is null or not Guid format");

                //Request Header


                //Check AccessKey
                if (!Request.HeaderKeyIsExist(Constants.AccessKey))
                    return BadRequest("AccessKey is required. Please add your header.");

                var accessKey = Request.GetHeaderValue(Constants.AccessKey);

                //Check AccessKey Value
                if (string.IsNullOrWhiteSpace(accessKey))
                    return BadRequest("AccessKey is null or empty. Please add your AccessKey.");

                #endregion


                #region Check App

                //Check App is Exist
                if (!_appService.IsExist(_appId))
                    return BadRequest("AppId is invalid. Please register your app.");


                //Get APP
                var app = _appService.GetAppById(_appId);

                //Check app is active
                if (!_appService.IsActive(app))
                    return NotFound();

                //Check Access Key
                if (!app.AccessKey.Equals(accessKey))
                    return Unauthorized();

                #endregion

                if (!_tokenService.IsExist(tokenModel.TokenKey))
                    return BadRequest("TokenKey is not exist.");

                var token = _tokenService.GetTokenByKey(tokenModel.TokenKey);

                //Verify
                if (!_tokenService.IsVerified(token))
                    return BadRequest("Signature invalid.");

                //Check Expire
                var isExpired = _tokenService.IsExpired(token);

                if (isExpired)
                    return BadRequest("Token is expired");

                var tokenResponse = new TokenResponse();

                var user = _userService.GetUserById(token.UserId);

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                tokenResponse.UserName = user.Name;
                tokenResponse.UserNameId = user.NameId;


                response = Ok(tokenResponse);

            }
            catch (Exception ex)
            {
                response = InternalServerError(ex);
            }
            return response;
        }
    }
}