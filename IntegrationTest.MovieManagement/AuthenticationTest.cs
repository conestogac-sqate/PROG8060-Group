using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IntegrationTest.MovieManagement
{
    [TestFixture]
    public class AuthenticationTest
    {
        [Test]
        public void UnauthorizedTest()
        {
            var client = new RestClient("https://localhost:44343/Session/Logout?username=testUser");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient("https://localhost:44343/Session/UpdateUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient("https://localhost:44343/Session/GetUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient("https://localhost:44343/Session/GetUsersAll");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient($"https://localhost:44343/Movie/Update?movieId=1&title=The Party&director=Test Director&genre=Test Genre&cast=Test Cast&year=2022&award=Test Award&is_show=true");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids=1");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient($"https://localhost:44343/Movie/GetByIds?ids=1");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient($"https://localhost:44343/Movie/GetByPrefix?prefix=title");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient($"https://localhost:44343/Movie/GetByAdvanceSearch?title=title&director=&genre=&cast=&year=-1&award=&isOnShow=2");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            client = new RestClient("https://localhost:44343/Movie/GetMoviesAll");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);
        }
    }
}
