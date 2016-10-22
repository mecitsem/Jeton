using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using Jeton.Services.TokenService;
using System.Linq;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            var mapper = _config.CreateMapper();
            var tokenList = _tokenService.GetTokens().AsEnumerable().Select(t => mapper.Map<TokenModel>(t)).ToList();
            return View(tokenList);
        }
    }
}