using System;
using System.Collections.Generic;
using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using System.Linq;
using System.Web.Mvc;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    //[Authorize(Roles = @"BUILTIN\Administrators")]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly MapperConfiguration _settingMapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingModel>());
        private readonly MapperConfiguration _tokenMapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Token, TokenModel>());
        private readonly MapperConfiguration _appMapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<App, AppModel>());

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

        public ActionResult Index()
        {

            #region Top Bar Count Items        

            BindTopBarCounts();

            #endregion

            #region Essential Settings

            BindSettings();

            #endregion

            return View();
        }

        [HttpGet]
        public JsonResult GetApps()
        {
            IEnumerable<AppModel> apps;
            try
            {
                var appMapper = _appMapperConfiguration.CreateMapper();
                apps = _appService.GetApps().AsEnumerable().Select(t => appMapper.Map<AppModel>(t)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return Json(apps, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTokens()
        {
            IEnumerable<TokenModel> tokens;
            try
            {
                var tokenMapper = _tokenMapperConfiguration.CreateMapper();
                tokens = _tokenService.GetTokens().AsEnumerable().Select(t => tokenMapper.Map<TokenModel>(t)).ToList();

            }
            catch (Exception)
            {

                throw;
            }
            return Json(tokens, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetTokenActivity()
        {

            try
            {
                var apps = _appService.GetApps();
                var tokens = _tokenService.GetTokens();
                return Json(tokens, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }



        private void BindTopBarCounts()
        {
            try
            {
                //App Count
                ViewBag.AppCount = _appService.GetApps().Count(a => !a.IsDeleted.HasValue || (a.IsDeleted == false));

                //Active Token Count
                ViewBag.TokenCount = _tokenService.GetActiveTokensCount();

                //User Count
                ViewBag.UserCount = _userService.GetUsers().Count();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void BindSettings()
        {
            try
            {
                var settingMapper = _settingMapperConfiguration.CreateMapper();
                var essentialSettings = _settingService.GetAllSettings()
                                                        .AsEnumerable()
                                                        .Where(s => s.IsEssential)
                                                        .Select(s => settingMapper.Map<SettingModel>(s))
                                                        .ToList();
                ViewBag.Settings = essentialSettings;
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}