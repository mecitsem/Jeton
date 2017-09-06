using System.Web.Mvc;
using Jeton.ClientApp.Helpers;

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
                var apiKey = ConfigHelper.GetAppSettingsValue("ApiKey");
                var apiUrl = ConfigHelper.GetAppSettingsValue("ApiUrl");

                ViewBag.AppId = appId;
                ViewBag.ApiKey = apiKey;
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