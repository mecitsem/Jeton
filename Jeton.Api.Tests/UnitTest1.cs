using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Jeton.Api.Tests
{
    [TestClass]
    public class JetonApiTest
    {
        [TestMethod]
        public void GenerateToken()
        {
            const string rootAppId = "a73a253a-41d3-e611-87d4-94659cb4c67e";

            var client = new RestClient {BaseUrl = new Uri("http://localhost:5555")};

            var request = new RestRequest
            {
                Resource = $"/api/token/generate/{rootAppId}",
                Method = Method.POST
            };


            //Header
            request.AddHeader("AccessKey", "eE9aSlltWVFBeXhxL3dGOU9VRnRmQThCUTJHNm5GKzJkRFo1dnJoZDZKTktaWFJ2Ymc9PQ==");

            //Body
            request.AddParameter("UserName", @"domain\msemerci");
            request.AddParameter("UserNameId", @"123456");

            var response = client.Execute(request);

            var token = JsonConvert.DeserializeObject<TokenModel>(response.Content);

            token.Should().NotBeNull();
            token.TokenKey.Should().NotBeNullOrEmpty();

        }
    }

    public class TokenModel
    {
        public string TokenKey { get; set; }

    }
}
