using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;


        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // GET: Token
        public async Task<ActionResult> Index()
        {
            
            var tokenList = (await _tokenService.GetActiveTokensAsync()).OrderByDescending(o => o.Created).Select(Mapper.Map<TokenModel>).ToList();
            return View(tokenList);
        }
    }
}