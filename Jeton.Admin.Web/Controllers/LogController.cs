using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // GET: Log
        public async Task<ActionResult> Index()
        {
            var logs = (await _logService.GetDailyLogsAsync()).Select(Mapper.Map<LogModel>).ToList();
            return View(logs);
        }

        public async Task<ActionResult> Daily()
        {
            var logs = (await _logService.GetDailyLogsAsync()).Select(Mapper.Map<LogModel>).ToList();
            return View(logs);
        }



        [HttpGet]
        public async Task<JsonResult> GetLogDetail(string id)
        {
            Log log = null;
            try
            {
                Guid logId;
                if (Guid.TryParse(id, out logId))
                    log = await _logService.GetByIdAsync(logId);
            }
            catch (Exception)
            {
                
                throw;
            }

            return Json(log, JsonRequestBehavior.AllowGet);
        }
    }
}