using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using Jeton.Services.AppService;
using Jeton.Services.TokenService;
using Jeton.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeton.Admin.Web.Controllers
{
    //[Authorize(Roles = @"BUILTIN\Administrators")]
    public class HomeController : Controller
    {
        private readonly IAppService _appService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private MapperConfiguration configApp = new MapperConfiguration(cfg => cfg.CreateMap<App, AppModel>());
        private MapperConfiguration configToken = new MapperConfiguration(cfg => cfg.CreateMap<Token, TokenModel>());

        public HomeController(IAppService appService, ITokenService tokenService, IUserService userService)
        {
            _appService = appService;
            _tokenService = tokenService;
            _userService = userService;
        }

        public ActionResult Index()
        {

            ViewBag.AppCount = _appService.GetApps().Count();
            ViewBag.TokenCount = _tokenService.GetActiveTokensCount();
            ViewBag.UserCount = _userService.GetUsers().Count();
            return View();
        }

        public ActionResult Apps()
        {
            var mapper = configApp.CreateMapper();
            var appList = _appService.GetApps().AsEnumerable().Select(a => mapper.Map<App, AppModel>(a)).ToList();
            return View(appList);
        }

        public ActionResult Tokens()
        {
            return View();
        }

    }
}