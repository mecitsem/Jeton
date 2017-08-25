using System;
using Jeton.Sdk.Models;
using Jeton.Sdk.ServiceModels;
using Newtonsoft.Json;
using System.Net;

namespace Jeton.Sdk
{
    /// <summary>
    /// You must use Newtonsoft.Json and RestSharp nuget packages  
    /// </summary>
    public class JetonClient
    {


        /// <summary>
        /// 
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId">Registered Application ID</param>
        /// <param name="accessKey">Registered Application AccessKey</param>
        /// <param name="apiUrl">Jeton Api Url</param>
        public JetonClient(string appId, string apiKey, string apiUrl)
        {
            ApiKey = apiKey;

            AppId = appId;

            Uri uri;
            if (!Uri.TryCreate(apiUrl, UriKind.Absolute, out uri))
                throw new ArgumentException("ApiUrl is not correct format. Please check it.");

            ApiUrl = apiUrl;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenResponse<Token> GenerateToken(User user)
        {
            var result = new TokenResponse<Token>();
            try
            {
                if (string.IsNullOrWhiteSpace(ApiKey))
                    throw new ArgumentNullException(nameof(ApiKey));

                if (string.IsNullOrWhiteSpace(AppId))
                    throw new ArgumentNullException(nameof(AppId));

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                Token token = null;
                using (var client = new WebClient())
                {
                    client.BaseAddress = ApiUrl;
                    client.Headers.Add("apiKey", ApiKey);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var res = client.UploadString($"/api/token/generate/{AppId}", "POST", JsonConvert.SerializeObject(user));
                    token = JsonConvert.DeserializeObject<Token>(res);
                }
               
                result.Data = token ?? throw new ArgumentException("Something went wrong");
                result.Status = true;

            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public TokenResponse<User> VerifyToken(Token token)
        {
            var result = new TokenResponse<User>();

            try
            {
                if (string.IsNullOrWhiteSpace(ApiKey))
                    throw new ArgumentNullException(nameof(ApiKey));

                if (string.IsNullOrWhiteSpace(AppId))
                    throw new ArgumentNullException(nameof(AppId));

                if (token == null)
                    throw new ArgumentNullException(nameof(token));

                User user = null;
                
                using (var client = new WebClient())
                {
                    client.BaseAddress = ApiUrl;
                    client.Headers.Add("apiKey", ApiKey);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var res = client.UploadString($"/api/token/check/{AppId}", JsonConvert.SerializeObject(token));
                    token = JsonConvert.DeserializeObject<Token>(res);
                }
                result.Data = user ?? throw new ArgumentException("Something went wrong");
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }


            return result;
        }
    }
}
