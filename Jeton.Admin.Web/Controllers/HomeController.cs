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
        private readonly MapperConfiguration _settingMapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingModel>().ReverseMap());
        private readonly MapperConfiguration _tokenMapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Token, TokenModel>().ReverseMap());
        private readonly MapperConfiguration _appMapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<App, AppModel>().ReverseMap());

        private readonly IAppService _appService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ISettingService _settingService;

        public HomeController(IAppService appService, ITokenService tokenService, IUserService userService, ISettingService settingService)
        {
            _appService = appService;
            _tokenService = tokenService;
            _userService = userService;
            _settingService = settingService;
        }

        public async Task<ActionResult> Index()
        {

            #region Top Bar Count Items        

            await BindTopBarCountsAsync();

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
                var appMapper = _appMapperConfiguration.CreateMapper();
                var allApps = await _appService.GetAllAsync();
                apps = allApps.Select(t => appMapper.Map<AppModel>(t)).ToList();
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
            IEnumerable<TokenModel> tokens;
            try
            {
                var tokenMapper = _tokenMapperConfiguration.CreateMapper();
                var allTokens = await _tokenService.GetAllAsync();
                tokens = allTokens.Select(t => tokenMapper.Map<TokenModel>(t)).ToList();

            }
            catch (Exception)
            {

                throw;
            }
            return Json(tokens, JsonRequestBehavior.AllowGet);

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
        private async Task BindTopBarCountsAsync()
        {
            try
            {
                //App Count
                ViewBag.AppCount = (await _appService.GetAllAsync()).Count(a => !a.IsDeleted.HasValue || (a.IsDeleted == false));

                //Active Token Count
                ViewBag.TokenCount = await _tokenService.GetActiveTokensCountAsync();

                //User Count
                ViewBag.UserCount = (await _userService.GetAllAsync()).Count();
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
                var settingMapper = _settingMapperConfiguration.CreateMapper();
                var essentialSettings = (await _settingService.GetAllAsync()).Where(s => s.IsEssential)
                                                        .Select(s => settingMapper.Map<SettingModel>(s))
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