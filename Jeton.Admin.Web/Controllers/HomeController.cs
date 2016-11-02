using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using Jeton.Services.AppService;
using Jeton.Services.SettingService;
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
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingModel>());
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

            ViewBag.AppCount = _appService.GetApps().Count(a => !a.IsDeleted.HasValue || (a.IsDeleted == false));
            ViewBag.TokenCount = _tokenService.GetActiveTokensCount();
            ViewBag.UserCount = _userService.GetUsers().Count();


            //Essential Settings
            var mapper = _config.CreateMapper();
            var essentialSettings = _settingService.GetAllSettings().AsEnumerable().Where(s => s.IsEssential).Select(s => mapper.Map<SettingModel>(s)).ToList();
            ViewBag.Settings = essentialSettings;
            return View();
        }

    }
}