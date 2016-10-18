using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Admin.Web.ViewModel;
using Jeton.Core.Entities;
using Jeton.Services.AppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeton.Admin.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IAppService _appService;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<App, AppModel>());

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        // GET: App
        public ActionResult Index()
        {
            var mapper = config.CreateMapper();
            var appList = _appService.GetApps().AsEnumerable().Select(a => mapper.Map<AppModel>(a)).ToList();
            return View(appList);
        }

        public ActionResult Detail(string Id)
        {
            Guid appId;
            if (string.IsNullOrEmpty(Id) || !Guid.TryParse(Id, out appId))
                return null;

            if (!_appService.IsExist(appId))
                return null;
            var mapper = config.CreateMapper();
            var app = mapper.Map<AppModel>(_appService.GetAppById(appId));

            return View(app);
        }
    }
}