using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Sdk.Models;
using Jeton.Sdk.ServiceModels;
using Newtonsoft.Json;
using RestSharp;

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
        public string AccessKey { get; set; }
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
        public JetonClient(string appId, string accessKey, string apiUrl)
        {
            AccessKey = accessKey;

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
                if (string.IsNullOrWhiteSpace(AccessKey))
                    throw new ArgumentNullException(nameof(AccessKey));

                if (string.IsNullOrWhiteSpace(AppId))
                    throw new ArgumentNullException(nameof(AppId));

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var client = new RestClient { BaseUrl = new Uri(this.ApiUrl) };

                var request = new RestRequest
                {
                    Resource = $"/api/token/generate/{AppId}",
                    Method = Method.POST
                };

                //Header
                request.AddHeader("AccessKey", AccessKey);

                //Body
                request.RequestFormat = DataFormat.Json;
                request.AddBody(user);

                //Execute
                var response = client.Execute(request);

                //Deserialize response content
                var token = JsonConvert.DeserializeObject<Token>(response.Content);

                result.Status = true;
                result.Error = string.Empty;
                result.Data = token;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Error = ex.Message;
                result.Data = null;
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
                if (string.IsNullOrWhiteSpace(AccessKey))
                    throw new ArgumentNullException(nameof(AccessKey));

                if (string.IsNullOrWhiteSpace(AppId))
                    throw new ArgumentNullException(nameof(AppId));

                if (token == null)
                    throw new ArgumentNullException(nameof(token));

                var client = new RestClient { BaseUrl = new Uri(this.ApiUrl) };

                var request = new RestRequest
                {
                    Resource = $"/api/token/check/{AppId}",
                    Method = Method.POST
                };

                //Header
                request.AddHeader("AccessKey", AccessKey);

                //Body
                request.RequestFormat = DataFormat.Json;
                request.AddBody(token);

                //Execute
                var response = client.Execute(request);

                //Deserialize response content
                var user = JsonConvert.DeserializeObject<User>(response.Content);

                result.Status = true;
                result.Error = string.Empty;
                result.Data = user;
            }
            catch (Exception ex)
            {

                result.Status = false;
                result.Error = ex.Message;
                result.Data = null;
            }


            return result;
        }
    }
}
