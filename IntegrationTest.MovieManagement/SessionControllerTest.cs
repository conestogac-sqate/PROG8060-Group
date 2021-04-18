using Newtonsoft.Json;
using NUnit.Framework;
using PROG8060_Group.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTest.MovieManagement
{
    [TestFixture]
    public class SessionControllerTest
    {
        private readonly string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImphY3F1ZWxpbmUiLCJuYmYiOjE2MTg3NzI0NTQsImV4cCI6MTYxOTM3NzI1NCwiaWF0IjoxNjE4NzcyNDU0fQ.4fjXSpCWLjz5NoPsHa-rLCXq1yzACgqZK69UlJrQkb0";

        [Test]
        public void LoginTest()
        {
            var client = new RestClient("https://localhost:44343/Session/Login?username=jacqueline&password=P@ssw0rd");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<UserInfo>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            // Prerequisite - Fail
            client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/Login?username=testUser&password=P@ssw0rd");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void LogoutTest()
        {
            // Prerequisite - Pass
            var client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/Login?username=testUser&password=P@ssw0rd");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/Logout?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            // Prerequisite - Fail
            client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/Logout?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void AddUserTest()
        {
            // Prerequisite - Pass
            var client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void UpdateUserTest()
        {
            // Prerequisite - Pass
            var client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/UpdateUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            // Prerequisite - Fail
            client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/UpdateUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void DeleteUserTest()
        {
            // Prerequisite - Pass
            var client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(sessionDto.Success);

            client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void GetUserTest()
        {
            // Prerequisite - Pass
            var client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/Login?username=testUser&password=P@ssw0rd");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/GetUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<UserInfo>>(response.Content);
            UserInfo userInfo = (UserInfo)sessionDto.Data;
            Assert.IsTrue(sessionDto.Success);
            Assert.AreEqual(userInfo.Name, "testUser");

            // Prerequisite - Fail
            client = new RestClient("https://localhost:44343/Session/DeleteUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/GetUser?username=testUser");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            sessionDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(sessionDto.Success);
        }

        [Test]
        public void GetUsersAllTest()
        {
            // Prerequisite - Pass
            var client = new RestClient("https://localhost:44343/Session/AddUser?username=testUser&password=P@ssw0rd&email=abc@abc.com&canCreate=true&canUpdate=true&canRead=true&canDelete=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/Login?username=testUser&password=P@ssw0rd");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);

            client = new RestClient("https://localhost:44343/Session/GetUsersAll");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            ApiResult sessionDto = JsonConvert.DeserializeObject<ApiSuccess<UserInfo[]>>(response.Content);
            UserInfo[] userInfo = (UserInfo[])sessionDto.Data;
            Assert.IsTrue(sessionDto.Success);
            Assert.IsTrue(userInfo.Length > 0);
        }
    }
}
