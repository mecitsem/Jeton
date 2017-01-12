using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jeton.Core.Common;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ISettingService _settingService;

        public TokenController(ITokenService tokenService, ISettingService settingService)
        {
            _tokenService = tokenService;
            _settingService = settingService;
        }

        // GET: Token
        public async Task<ActionResult> Index()
        {

            var tokenList = (await _tokenService.GetActiveTokensAsync()).OrderByDescending(o => o.Created).Select(Mapper.Map<TokenModel>).ToList();

            var tokenDurationSettingsValue = (await _settingService.GetSettingByNameAsync(Constants.Settings.TokenDuration))?.Value;

            var tokenDuration = 0;

            if (int.TryParse(tokenDurationSettingsValue, out tokenDuration))
                ViewBag.TokenDuration = tokenDuration;

            return View(tokenList);
        }
    }
}