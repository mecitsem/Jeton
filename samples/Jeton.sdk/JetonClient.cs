using System;
using System.Net.Http;
using System.Text;

namespace Jeton.Sdk
{


    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.6.1.0")]
    public partial class JetonClient : IJetonClient
    {
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;
        private string _baseUrl = "";
        private string _apiKey;
        public JetonClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }
        public string ApiKey {
            get { return _apiKey; }
            set { _apiKey = value; }
        }
        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            client.DefaultRequestHeaders.Add("apiKey", _apiKey);
        }
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder)
        {
            client.DefaultRequestHeaders.Add("apiKey", _apiKey);
        }
        /// <returns>OK</returns>
        /// <exception cref="JetonException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<JetonToken> GenerateAsync(string appId, JetonIdentity identity)
        {
            return GenerateAsync(appId, identity, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<JetonToken> GenerateAsync(string appId, JetonIdentity identity, System.Threading.CancellationToken cancellationToken)
        {
            if (appId == null)
                throw new System.ArgumentNullException("appId");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append("/api/token/generate/{appId}");
            urlBuilder_.Replace("{appId}", System.Uri.EscapeDataString(System.Convert.ToString(appId, System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(identity, _settings.Value));
                    content_.Headers.ContentType.MediaType = "application/json";
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(JetonToken);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<JetonToken>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception)
                            {
                                throw new JetonException("Could not deserialize the response body.", status_, responseData_, headers_, exception);
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new JetonException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }

                        return default(JetonToken);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        /// <returns>OK</returns>
        /// <exception cref="JetonException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<JetonIdentity> CheckAsync(string appId, JetonToken token)
        {
            return CheckAsync(appId, token, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="JetonException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<JetonIdentity> CheckAsync(string appId, JetonToken token, System.Threading.CancellationToken cancellationToken)
        {
            if (appId == null)
                throw new System.ArgumentNullException("appId");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append("/api/token/check/{appId}");
            urlBuilder_.Replace("{appId}", System.Uri.EscapeDataString(System.Convert.ToString(appId, System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(token, _settings.Value));
                    content_.Headers.ContentType.MediaType = "application/json";
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(JetonIdentity);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<JetonIdentity>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception)
                            {
                                throw new JetonException("Could not deserialize the response body.", status_, responseData_, headers_, exception);
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new JetonException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }

                        return default(JetonIdentity);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

    }



  




}
