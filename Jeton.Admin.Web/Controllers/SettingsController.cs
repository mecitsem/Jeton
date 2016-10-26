using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using Jeton.Services.SettingService;

namespace Jeton.Admin.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingModel>());
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // GET: Settings
        public ActionResult Index()
        {
            var mapper = _config.CreateMapper();
            var settings = _settingService.GetAllSettings().AsEnumerable().Select(s=> mapper.Map<SettingModel>(s)).ToList();
            return View(settings);
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(string id)
        {
            Guid settingId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out settingId))
                return null;

            return View();
        }

        public ActionResult Edit(SettingModel model)
        {
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            return View();
        }
    }
}