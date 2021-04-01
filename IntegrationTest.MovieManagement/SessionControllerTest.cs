using Newtonsoft.Json;
using NUnit.Framework;
using PROG8060_Group.Models;
using RestSharp;

namespace IntegrationTest.MovieManagement
{
    [TestFixture]
    public class SessionControllerTest
    {
        [Test]
        public void LoginTest()
        {
            var client = new RestClient("https://localhost:44343/Session/Login?username=user1&password=P@ssw0rd");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/Login?username=user2&password=P@ssw0rd");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void LogoutTest()
        {
            var client = new RestClient("https://localhost:44343/Session/Logout?username=user1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/Logout?username=user2");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void AddUserTest()
        {
            var client = new RestClient("https://localhost:44343/Session/AddUser?username=user3&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/AddUser?password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void UpdateUserTest()
        {
            var client = new RestClient("https://localhost:44343/Session/UpdateUser?username=user1&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            IRestResponse response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/UpdateUser?password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void DeleteUserTest()
        {
            var client = new RestClient("https://localhost:44343/Session/DeleteUser?username=user3");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/DeleteUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void GetUserTest()
        {
            var client = new RestClient("https://localhost:44343/Session/GetUser?username=user1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/GetUser");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }
    }
}
