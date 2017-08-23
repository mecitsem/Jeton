using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Jeton.ClientApp.Helpers;
using Jeton.ClientApp.Models;
using Jeton.Sdk;
using Jeton.Sdk.Models;

namespace Jeton.ClientApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            var tokenKey = Request.QueryString.GetValues("TokenKey")?.FirstOrDefault();

            return !string.IsNullOrWhiteSpace(tokenKey) ? JwtLogin() : View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return Login();


            var appId = ConfigHelper.GetAppSettingsValue("AppId");
            var accessKey = ConfigHelper.GetAppSettingsValue("AccessKey");
            var apiUrl = ConfigHelper.GetAppSettingsValue("ApiUrl");

            var jetonClient = new JetonClient(appId, accessKey, apiUrl);
            var response = jetonClient.VerifyToken(new Token());



            var isAuth = SetAuthCookie(model, model.RememberMe);

            if (!isAuth) return Login();

            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }




        }



        public ActionResult JwtLogin()
        {
            var appId = ConfigHelper.GetAppSettingsValue("AppId");
            var accessKey = ConfigHelper.GetAppSettingsValue("AccessKey");
            var apiUrl = ConfigHelper.GetAppSettingsValue("ApiUrl");



            var tokenKey = Request.QueryString.GetValues("TokenKey")?.FirstOrDefault();
            var returnUrl = Request.QueryString.GetValues("ReturnUrl")?.FirstOrDefault();

            if (!string.IsNullOrEmpty(tokenKey))
            {

                var jetonClient = new JetonClient(appId, accessKey, apiUrl);
                var response = jetonClient.VerifyToken(new Token()
                {
                    TokenKey = tokenKey
                });

                if (response.Status)
                {
                    var isAuth = SetAuthCookie(new LoginViewModel()
                    {
                        Email = response.Data.UserName
                    }, true);

                    return Redirect(returnUrl);
                }
            }

            return null;
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        [NonAction]
        public bool SetAuthCookie(LoginViewModel userModel, bool remember)
        {
            bool result;
            try
            {
                FormsAuthentication.SignOut();

                var data = JsonHelper.Serialize(userModel);



                var durationInHours = remember ? 240 : 8;
                var authTicket = new FormsAuthenticationTicket(
                    1,                                          // version number
                    userModel.Email,                            // name of the cookie
                    DateTime.Now,                               // issue date
                    DateTime.Now.AddHours(durationInHours),     // expiration
                    true,                                       // survives browser sessions
                    data);                                      // custom data (serialized)

                var ticket = FormsAuthentication.Encrypt(authTicket);
                var cookie = FormsAuthentication.GetAuthCookie(FormsAuthentication.FormsCookieName, remember);
                cookie.Value = ticket;
                Response.Cookies.Add(cookie);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

    }
}