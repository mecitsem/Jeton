using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Admin.Web.ViewModel;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingModel>().ReverseMap());
        private readonly MapperConfiguration _configModel = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingViewModel>().ReverseMap());
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // GET: Settings
        public async Task<ActionResult> Index()
        {
            var mapper = _config.CreateMapper();
            var settings = (await _settingService.GetAllAsync()).Select(s => mapper.Map<SettingModel>(s)).ToList();
            return View(settings);
        }

        #region CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SettingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _settingService.IsExistAsync(model.Name))
                throw new ArgumentException("This setting is already exists.");
            try
            {
                var newSetting = new Setting()
                {
                    Name = model.Name,
                    Value = model.Value,
                    ValueType = model.ValueType,
                };

                var setting = await _settingService.CreateAsync(newSetting);
                return RedirectToAction("Detail", new { id = setting.Id });
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
        public async Task<ActionResult> Detail(string id)
        {
            Guid settingId;
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out settingId))
                return View();

            if (!await _settingService.IsExistAsync(settingId))
                return View();

            var setting = await _settingService.GetByIdAsync(settingId);

            var mapper = _configModel.CreateMapper();

            var model = mapper.Map<SettingViewModel>(setting);

            return View(model);
        }
        #endregion

        #region EDIT
        public async Task<ActionResult> Edit(string id)
        {
            Guid settingId;
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out settingId))
                return HttpNotFound("SettingId is null or it's not a Guid.");

            if (!await _settingService.IsExistAsync(settingId))
                return HttpNotFound("Setting is not exist.");

            var mapper = _configModel.CreateMapper();
            var setting = mapper.Map<SettingViewModel>(await _settingService.GetByIdAsync(settingId));

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SettingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!_settingService.IsExist(model.Id))
                return HttpNotFound("Setting is not exist.");

            try
            {
                var setting = await _settingService.GetByIdAsync(model.Id);

                setting.Value = model.Value;
                setting.ValueType = model.ValueType;
                setting.Description = model.Description;

                await _settingService.UpdateAsync(setting);

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

        public async Task<ActionResult> Delete(string id)
        {
            Guid settingId;
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out settingId))
                return HttpNotFound("SettingId is null or it's not a Guid.");

            if (!await _settingService.IsExistAsync(settingId))
                return HttpNotFound("Setting is not exist.");

            var mapper = _configModel.CreateMapper();
            var setting = mapper.Map<SettingViewModel>(await _settingService.GetByIdAsync(settingId));

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_settingService.IsExist(id))
                        return HttpNotFound("Setting is not exist.");

                    var setting = await _settingService.GetByIdAsync(id);

                    if (setting.IsEssential)
                        return new HttpStatusCodeResult(403, "This setting is essential. You can not delete it.");

                    await _settingService.DeleteAsync(id);
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