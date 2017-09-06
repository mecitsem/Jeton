using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Jeton.ClientApp.Helpers;
using Jeton.ClientApp.Models;
using Jeton.Sdk;
using System.Threading.Tasks;

namespace Jeton.ClientApp.Controllers
{
    public class AccountController : Controller
    {
        public async Task<ActionResult> Login()
        {
            var tokenKey = Request.QueryString.GetValues("AccessToken")?.FirstOrDefault();

            return !string.IsNullOrWhiteSpace(tokenKey) ? await JwtLogin() : View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return await Login();

            var isAuth = SetAuthCookie(model, model.RememberMe);

            if (!isAuth) return await Login();

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



        public async Task<ActionResult> JwtLogin()
        {
            var appId = ConfigHelper.GetAppSettingsValue("AppId");
            var apiKey = ConfigHelper.GetAppSettingsValue("ApiKey");
            var apiUrl = ConfigHelper.GetAppSettingsValue("ApiUrl");



            var accessToken = Request.QueryString.GetValues("AccessToken")?.FirstOrDefault();
            var returnUrl = Request.QueryString.GetValues("ReturnUrl")?.FirstOrDefault();

            if (!string.IsNullOrEmpty(accessToken))
            {

                var client = new JetonClient()
                {
                    ApiKey = apiKey,
                    BaseUrl = apiUrl
                };

                var identity = await client.CheckAsync(appId, new JetonToken()
                {
                    AccessToken = accessToken
                });

                if (identity != null)
                {
                    var isAuth = SetAuthCookie(new LoginViewModel()
                    {
                        Email = identity.UserName
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