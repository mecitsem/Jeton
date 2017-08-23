using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Admin.Web.Controllers
{
    public class ToolsController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ISettingService _settingService;
        private readonly IAppService _appService;

        public ToolsController(ITokenService tokenService, ISettingService settingService, IAppService appService)
        {
            _tokenService = tokenService;
            _settingService = settingService;
            _appService = appService;
        }


        // GET: Tool
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.RootApps = (await _appService.GetAllAsync()).Where(a => a.IsRoot).OrderBy(a => a.Name).Select(Mapper.Map<AppModel>).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> VerifyToken(string tokenKey)
        {
            var result = new JsonResponseModel();
            try
            {
                var serviceResult = await _tokenService.IsVerifiedAsync(tokenKey);

                result.Result = true;
                result.Content = serviceResult ? "Verified" : "Invalid";
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> DecodeToken(string tokenKey)
        {
            var result = new JsonResponseModel();
            try
            {
                var token = await _tokenService.GetTokenByKeyAsync(tokenKey);

                var serviceResult = await _tokenService.DecodeAsync(token);

                if (string.IsNullOrWhiteSpace(serviceResult))
                    throw new ArgumentException("Invalid token");

                result.Result = true;
                result.Content = serviceResult;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> CheckToken(string tokenKey)
        {
            var result = new JsonResponseModel();
            try
            {
                var token = await _tokenService.GetTokenByKeyAsync(tokenKey);

                var serviceResult = await _tokenService.IsExpiredAsync(token);

                result.Result = true;
                result.Content = serviceResult;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}