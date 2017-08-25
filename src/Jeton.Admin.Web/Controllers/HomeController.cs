using System;
using System.Collections.Generic;
using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    //[Authorize(Roles = @"BUILTIN\Administrators")]
    //[Authorize]
    public class HomeController : Controller
    {

        private readonly IAppService _appService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        public HomeController(IAppService appService, ITokenService tokenService, IUserService userService, ISettingService settingService,ILogService logService)
        {
            _appService = appService;
            _tokenService = tokenService;
            _userService = userService;
            _settingService = settingService;
            _logService = logService;
        }

        public async Task<ActionResult> Index()
        {

            #region Top Bar Count Items        

            await AppCountAsync();
            await TokenCountAsync();
            await UserCountAsync();
            await LogsCountAsync();
           
            #endregion

            #region Essential Settings

            await BindSettingsAsync();

            #endregion

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetApps()
        {
            IEnumerable<AppModel> apps = null;
            try
            {
                var allApps = await _appService.GetAllAsync();
                apps = allApps.Select(Mapper.Map<AppModel>).ToList();
            }
            catch
            {
                // ignored
            }
            return Json(apps, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetTokens()
        {
            object data = null;
            try
            {
                var allTokens = await _tokenService.GetAllAsync();
                data = allTokens.Select(Mapper.Map<TokenModel>).ToList();
            }
            catch (Exception ex)
            {
                data = ex.Message;
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<JsonResult> GetTokenActivity()
        {

            try
            {
                //var apps = await _appService.GetAllAsync();
                var tokens = await _tokenService.GetAllAsync();
                return Json(tokens, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }


        [NonAction]
        private async Task AppCountAsync()
        {
            try
            {
                //App Count
                ViewBag.AppCount = (await _appService.GetAllAsync()).Count(a => !a.IsDeleted.HasValue || (a.IsDeleted == false));
            }
            catch
            {
                // ignored
            }
        }


        [NonAction]
        private async Task UserCountAsync()
        {
            try
            {
                //User Count
                ViewBag.UserCount = (await _userService.GetAllAsync()).Count();
            }
            catch
            {
                // ignored
            }
        }

        [NonAction]
        private async Task TokenCountAsync()
        {
            try
            {
                //Active Token Count
                ViewBag.TokenCount = await _tokenService.GetActiveTokensCountAsync();
            }
            catch
            {
                // ignored
            }
        }

        private async Task LogsCountAsync()
        {
            try
            {
                ViewBag.DailyLogsCount = await _logService.GetDailyLogsCountAsync();
            }
            catch 
            {
                // ignored
            }
        }


        [NonAction]
        private async Task BindSettingsAsync()
        {
            try
            {
        
                var essentialSettings = (await _settingService.GetAllAsync()).Where(s => s.IsEssential)
                                                        .Select(Mapper.Map<SettingModel>)
                                                        .ToList();
                ViewBag.Settings = essentialSettings;
            }
            catch
            {
                // ignored
            }
        }


    }
}