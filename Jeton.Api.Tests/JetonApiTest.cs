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

        const string ApiUrl = "http://jeton.io";

        [TestMethod]
        public void Generate_Token()
        {
            

            var client = new RestClient {BaseUrl = new Uri(ApiUrl) };

            var request = new RestRequest
            {
                Resource = $"/api/token/generate/{RootApp.Id}",
                Method = Method.POST
            };


            //Header
            request.AddHeader("AccessKey", RootApp.AccessKey);

            //Body
            var userModel = new UserModel()
            {
                UserName = @"domain\username",
                UserNameId = @"123456789"
            };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(userModel);
            var response = client.Execute(request);

            var token = JsonConvert.DeserializeObject<TokenModel>(response.Content);

            token.Should().NotBeNull();
            token.TokenKey.Should().NotBeNullOrEmpty();

        }

        [TestMethod]
        public void Generate_Token_And_Check()
        {
           

            var client = new RestClient { BaseUrl = new Uri(ApiUrl) };

            var request = new RestRequest
            {
                Resource = $"/api/token/generate/{RootApp.Id}",
                Method = Method.POST
            };


            //Header
            request.AddHeader("AccessKey", RootApp.AccessKey);

            //Body
            var userModel = new UserModel()
            {
                UserName = @"domain\username",
                UserNameId = @"123456789"
            };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(userModel);
            var response = client.Execute(request);

            var token = JsonConvert.DeserializeObject<TokenModel>(response.Content);

            token.Should().NotBeNull();
            token.TokenKey.Should().NotBeNullOrEmpty();

            ////// CHECK /////////
            var request2 = new RestRequest
            {
                Resource = $"/api/token/check/{ClientApp.Id}",
                Method = Method.POST
            };

            request2.AddHeader("AccessKey", ClientApp.AccessKey);

            //Body
            var tokenModel = new TokenModel()
            {
                TokenKey = token.TokenKey
            };

            request2.RequestFormat = DataFormat.Json;
            request2.AddBody(tokenModel);
            var response2 = client.Execute(request2);

            var userModel2 = JsonConvert.DeserializeObject<UserModel>(response2.Content);

            userModel2.Should().NotBeNull();
            userModel2.UserName.Should().NotBeNullOrEmpty();

            userModel2.UserNameId.Should().Be(userModel.UserNameId);
        }


        
    }

    public class TokenModel
    {
        public string TokenKey { get; set; }

    }

    public class UserModel
    {
        public string UserName { get; set; }
        public string UserNameId { get; set; }
    }

    public class RootApp
    {
        public static readonly string Id = "a73a253a-41d3-e611-87d4-94659cb4c67e";

        public static readonly string AccessKey =
            "eE9aSlltWVFBeXhxL3dGOU9VRnRmQThCUTJHNm5GKzJkRFo1dnJoZDZKTktaWFJ2Ymc9PQ==";
    }

    public class ClientApp
    {
        public static readonly string Id = "1a2e8d28-42d3-e611-87d4-94659cb4c67e";

        public static readonly string AccessKey =
            "d2pJNVNYeFZPYTE4SytZd2Fha1hiSDdPblRLMjBRdERacUNNdmZZaEFmQktaWFJ2Ymc9PQ==";
    }
}
