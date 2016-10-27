using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Admin.Web.ViewModel;
using Jeton.Core.Entities;
using Jeton.Services.SettingService;

namespace Jeton.Admin.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingModel>());
        private readonly MapperConfiguration _configModel = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingViewModel>());
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // GET: Settings
        public ActionResult Index()
        {
            var mapper = _config.CreateMapper();
            var settings = _settingService.GetAllSettings().AsEnumerable().Select(s => mapper.Map<SettingModel>(s)).ToList();
            return View(settings);
        }

        #region CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SettingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_settingService.IsExist(model.Name))
                throw new ArgumentException("This setting is already exists.");
            try
            {
                var newSetting = new Setting()
                {
                    Name = model.Name,
                    Value = model.Value,
                    ValueType = model.ValueType,
                };

                var setting = _settingService.Insert(newSetting);
                return RedirectToAction("Detail", new { id = setting.SettingID });
            }
            catch (Exception ex)
            {
                //TODO:Log
                ModelState.AddModelError("Create", ex);
            }

            return View(model);
        }
        #endregion

        #region DETEAIL
        public ActionResult Detail(string id)
        {
            Guid settingId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out settingId))
                return View();

            if (!_settingService.IsExist(settingId))
                return View();

            var setting = _settingService.GetSettingById(settingId);

            var mapper = _configModel.CreateMapper();

            var model = mapper.Map<SettingViewModel>(setting);

            return View(model);
        }
        #endregion

        #region EDIT
        public ActionResult Edit(string id)
        {
            Guid settingId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out settingId))
                return HttpNotFound("SettingId is null or it's not a Guid.");

            if (!_settingService.IsExist(settingId))
                return HttpNotFound("Setting is not exist.");

            var mapper = _configModel.CreateMapper();
            var setting = mapper.Map<SettingViewModel>(_settingService.GetSettingById(settingId));

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SettingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!_settingService.IsExist(model.SettingID))
                return HttpNotFound("Setting is not exist.");

            try
            {
                var setting = _settingService.GetSettingById(model.SettingID);

                setting.Value = model.Value;
                setting.ValueType = model.ValueType;

                _settingService.Update(setting);

                return RedirectToAction("Index", "Settings");
            }
            catch (Exception ex)
            {
                //TODO:Log
                ModelState.AddModelError("Edit", ex);
            }

            return View(model);
        }
        #endregion

        #region DELETE

        public ActionResult Delete(string id)
        {
            Guid settingId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out settingId))
                return HttpNotFound("SettingId is null or it's not a Guid.");

            if (!_settingService.IsExist(settingId))
                return HttpNotFound("Setting is not exist.");

            var mapper = _configModel.CreateMapper();
            var setting = mapper.Map<SettingViewModel>(_settingService.GetSettingById(settingId));

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_settingService.IsExist(id))
                        return HttpNotFound("Setting is not exist.");
                    _settingService.Delete(id);
                }
            }
            catch (Exception ex)
            {
                //TODO:Log
                ModelState.AddModelError("ChangeStatus", ex);
            }
            return RedirectToAction("Index", "Settings");
        }
        #endregion



    }
}