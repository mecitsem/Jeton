using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using Jeton.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeton.Admin.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>());

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            var mapper = config.CreateMapper();
            var userList = _userService.GetUsers().AsEnumerable().Select(a => mapper.Map<UserModel>(a)).ToList();
            return View(userList);
        }
    }
}