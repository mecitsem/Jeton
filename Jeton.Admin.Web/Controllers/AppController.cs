using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Admin.Web.ViewModel;
using Jeton.Core.Entities;
using Jeton.Services.AppService;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Jeton.Admin.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IAppService _appService;
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => cfg.CreateMap<App, AppModel>());
        private readonly MapperConfiguration _configModel = new MapperConfiguration(cfg => cfg.CreateMap<App, AppViewModel>());
        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        // GET: App
        public ActionResult Index(bool? active)
        {
            ViewBag.AppStatus = active.HasValue ? (active.Value ? "Active" : "Inactive") : "All";
            var mapper = _config.CreateMapper();
            var appList = _appService.GetApps().AsEnumerable().
                            Where(a => !active.HasValue || (active.Value ?
                                !a.IsDeleted.HasValue || (a.IsDeleted.Value == false) :
                                a.IsDeleted.HasValue && a.IsDeleted.Value)).Select(a => mapper.Map<AppModel>(a)).ToList();
            return View(appList);
        }

        public ActionResult Detail(string id)
        {
            Guid appId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out appId))
                return null;

            if (!_appService.IsExist(appId))
                return null;
            var mapper = _config.CreateMapper();
            var app = mapper.Map<AppModel>(_appService.GetAppById(appId));

            return View(app);
        }

        public ActionResult Edit(string id)
        {
            Guid appId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out appId))
                return HttpNotFound("AppId is null or it's not a Guid.");

            if (!_appService.IsExist(appId))
                return HttpNotFound("AppId is not exist.");

            var mapper = _configModel.CreateMapper();
            var app = mapper.Map<AppViewModel>(_appService.GetAppById(appId));

            return View(app);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppId, AccessKey, Name, IsRoot")] AppViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!_appService.IsExist(model.AppId))
                return View(model);
            try
            {
                //Get App
                var app = _appService.GetAppById(model.AppId);

                //Fields
                app.AccessKey = model.AccessKey;
                //app.Name = model.Name;
                app.IsRoot = model.IsRoot;

                //Update
                _appService.Update(app);

                return RedirectToAction("Index", "App");
            }
            catch (Exception ex)
            {
                //TODO:Log
                ModelState.AddModelError("Edit", ex);
            }
            return View(model);

        }

        [HttpGet]
        public JsonResult GenerateAccessKey()
        {
            return Json(_appService.GenerateAccessKey(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
            var model = new AppViewModel { AccessKey = _appService.GenerateAccessKey() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var newApp = new App
                {
                    AccessKey = model.AccessKey,
                    Name = model.Name,
                    IsRoot = model.IsRoot
                };

                var app = _appService.Insert(newApp);
                return RedirectToAction("Detail", new { id = app.AppID });
            }
            catch (Exception ex)
            {
                //TODO:Log
                ModelState.AddModelError("Create", ex);
            }

            return View(model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(string id)
        {
            Guid appId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out appId))
                return HttpNotFound("AppId is null or it's not a Guid.");

            if (!_appService.IsExist(appId))
                return HttpNotFound("AppId is not exist.");
            try
            {
                var app = _appService.GetAppById(appId);
                app.IsDeleted = !app.IsDeleted;
                _appService.Update(app);
            }
            catch (Exception ex)
            {
                //TODO:Log
                ModelState.AddModelError("ChangeStatus", ex);
            }


            return RedirectToAction("Index", "App");
        }
    }
}