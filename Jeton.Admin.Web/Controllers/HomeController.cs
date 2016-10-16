using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jeton.Services.AppService;
using Jeton.Services.TokenService;
using Jeton.Services.UserService;

namespace Jeton.Admin.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppService _appService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public HomeController(IAppService appService, ITokenService tokenService, IUserService userService)
        {
            _appService = appService;
            _tokenService = tokenService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            ViewBag.AppCount = _appService.GetApps().Count();
            ViewBag.TokenCount = _tokenService.GetLiveTokens().Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}