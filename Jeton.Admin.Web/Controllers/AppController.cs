using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Admin.Web.ViewModel;
using Jeton.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IAppService _appService;
      

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        // GET: App
        public async Task<ActionResult> Index(bool? active)
        {
            ViewBag.AppStatus = active.HasValue ? (active.Value ? "Active" : "Inactive") : "All";
            var apps = await _appService.GetAllAsync();
            var appList = apps.Where(a => !active.HasValue || (active.Value ? !a.IsDeleted.HasValue || (a.IsDeleted.Value == false) :
                                a.IsDeleted.HasValue && a.IsDeleted.Value)).Select(Mapper.Map<AppModel>).ToList();

            return View(appList);
        }

        public async Task<ActionResult> Detail(string id)
        {
            Guid appId;
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out appId))
                return View();

            if (!await _appService.IsExistAsync(appId))
                return View();

            var app = Mapper.Map<AppModel>(await _appService.GetByIdAsync(appId));

            return View(app);
        }

        public async Task<ActionResult> Edit(string id)
        {
            Guid appId;
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out appId))
                return HttpNotFound("AppId is null or it's not a Guid.");

            if (!await _appService.IsExistAsync(appId))
                return HttpNotFound("AppId is not exist.");

            var app = Mapper.Map<AppViewModel>(await _appService.GetByIdAsync(appId));

            return View(app);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, AccessKey, Name, IsRoot")] AppViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!await _appService.IsExistAsync(model.Id))
                return View(model);
            try
            {
                //Get App
                var app = await _appService.GetByIdAsync(model.Id);

                //Fields
                app.AccessKey = model.AccessKey;
                //app.Name = model.Name;
                app.IsRoot = model.IsRoot;

                //Update
                await _appService.UpdateAsync(app);

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
        public async Task<ActionResult> Create(AppViewModel model)
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

                var app = await _appService.CreateAsync(newApp);
                return RedirectToAction("Detail", new { id = app.Id });
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
        public async Task<ActionResult> ChangeStatus(string id)
        {
            Guid appId;
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out appId))
                return HttpNotFound("AppId is null or it's not a Guid.");

            if (!_appService.IsExist(appId))
                return HttpNotFound("AppId is not exist.");
            try
            {
                var app = await _appService.GetByIdAsync(appId);
                app.IsDeleted = !app.IsDeleted ?? true;
                await _appService.UpdateAsync(app);
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