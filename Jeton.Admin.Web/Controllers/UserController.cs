using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>());

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var mapper = _config.CreateMapper();
            var userList =(await _userService.GetAllAsync()).Select(a => mapper.Map<UserModel>(a)).ToList();
            return View(userList);
        }
    }
}