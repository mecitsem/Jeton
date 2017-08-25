using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jeton.RootApp.Helpers;
using Jeton.Sdk;
using Jeton.Sdk.Models;
using Microsoft.AspNet.Identity;

namespace Jeton.RootApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return View();

            try
            {

                var appId = ConfigHelper.GetAppSettingsValue("AppId");
                var apiKey = ConfigHelper.GetAppSettingsValue("ApiKey");
                var apiUrl = ConfigHelper.GetAppSettingsValue("ApiUrl");

                ViewBag.AppId = appId;
                ViewBag.AccessKey = apiKey;
                ViewBag.ApiUrl = apiUrl;

                var jetonClient = new JetonClient(appId, apiKey, apiUrl);
                var user = new User()
                {
                    UserName = User.Identity.Name,
                    UserNameId = User.Identity.GetUserId()
                };

                var response = jetonClient.GenerateToken(user);
                if (response.Status)
                {
                    Session["AccessToken"] = response.Data.AccessToken;
                    ViewBag.Token = response.Data.AccessToken;
                }
                else
                {
                    ViewBag.Error = response.Error;
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