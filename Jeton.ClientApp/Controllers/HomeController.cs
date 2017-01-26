using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jeton.ClientApp.Helpers;
using Jeton.Sdk;
using Jeton.Sdk.Models;
using Microsoft.Ajax.Utilities;

namespace Jeton.ClientApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var appId = ConfigHelper.GetAppSettingsValue("AppId");
                var accessKey = ConfigHelper.GetAppSettingsValue("AccessKey");
                var apiUrl = ConfigHelper.GetAppSettingsValue("ApiUrl");

                ViewBag.AppId = appId;
                ViewBag.AccessKey = accessKey;
                ViewBag.ApiUrl = apiUrl;
            }
            catch 
            {
                
               //ignored
            }
            return View();
        }
    }
}