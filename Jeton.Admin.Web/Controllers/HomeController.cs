using Jeton.Services.AppService;
using Jeton.Services.TokenService;
using Jeton.Services.UserService;
using System.Linq;
using System.Web.Mvc;

namespace Jeton.Admin.Web.Controllers
{
    //[Authorize(Roles = @"BUILTIN\Administrators")]
    //[Authorize]
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
            ViewBag.TokenCount = _tokenService.GetActiveTokensCount();
            ViewBag.UserCount = _userService.GetUsers().Count();
            return View();
        }

    }
}