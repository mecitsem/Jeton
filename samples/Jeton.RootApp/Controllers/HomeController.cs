using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jeton.RootApp.Helpers;
using Jeton.Sdk;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Jeton.RootApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return View();

            try
            {

                var appId = ConfigHelper.GetAppSettingsValue("AppId");
                var apiKey = ConfigHelper.GetAppSettingsValue("ApiKey");
                var apiUrl = ConfigHelper.GetAppSettingsValue("ApiUrl");

                ViewBag.AppId = appId;
                ViewBag.ApiKey = apiKey;
                ViewBag.ApiUrl = apiUrl;

                var client = new JetonClient()
                {
                    BaseUrl = apiUrl
                };


                var user = new JetonIdentity()
                {
                    UserName = User.Identity.Name,
                    UserNameId = User.Identity.GetUserId()
                };

                var token = await client.GenerateAsync(appId, user);
                if (token != null)
                {
                    Session["AccessToken"] = token.AccessToken;
                    ViewBag.Token = token.AccessToken;
                }
                else
                {
                    ViewBag.Error = "Token is null";
                }

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
            }

            return View();
        }

        public void ClientRedirect()
        {
            var token = Session["AccessToken"]?.ToString();
            var redirectUrl = Request.QueryString.GetValues("redirectUrl")?.FirstOrDefault();
            Uri uri;
            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(redirectUrl) && Uri.TryCreate(redirectUrl, UriKind.Absolute, out uri))
            {

                var uriBuilder = new UriBuilder(uri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["AccessToken"] = token;
                uriBuilder.Query = query.ToString();
                redirectUrl = uriBuilder.ToString();
            }


            if (!string.IsNullOrWhiteSpace(redirectUrl))
                Response.Redirect(redirectUrl);
        }

    }
}