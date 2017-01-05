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
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => cfg.CreateMap<Token, TokenModel>());

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // GET: Token
        public async Task<ActionResult> Index()
        {
            var mapper = _config.CreateMapper();
            var tokenList = (await _tokenService.GetAllAsync()).Select(t => mapper.Map<TokenModel>(t)).ToList();
            return View(tokenList);
        }
    }
}