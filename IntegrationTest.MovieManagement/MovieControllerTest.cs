using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PROG8060_Group.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTest.MovieManagement
{
    [TestFixture]
    public class MovieControllerTest
    {
        private readonly string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImphY3F1ZWxpbmUiLCJuYmYiOjE2MTg3NzI0NTQsImV4cCI6MTYxOTM3NzI1NCwiaWF0IjoxNjE4NzcyNDU0fQ.4fjXSpCWLjz5NoPsHa-rLCXq1yzACgqZK69UlJrQkb0";

        [Test]
        public void AddMovieTest()
        {
            int movieId = -1;
            var client = new RestClient("https://localhost:44385/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            Assert.IsTrue(movieDto.Success);
            movieId = (int)movieDto.Data;

            client = new RestClient("https://localhost:44385/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);

            // Clean Up
            client = new RestClient($"https://localhost:44385/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);
        }

        [Test]
        public void UpdateMovieTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44385/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            Assert.IsTrue(movieDto.Success);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44385/Movie/Update?movieId={movieId}&title=The Party&director=Test Director&genre=Test Genre&cast=Test Cast&year=2022&award=Test Award&is_show=true");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            // Clean Up
            client = new RestClient($"https://localhost:44385/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);
        }

        [Test]
        public void DeleteMovieTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44385/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            Assert.IsTrue(movieDto.Success);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44385/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44385/Movie/Delete?ids=999999999");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByIdsTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44385/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44385/Movie/GetByIds?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(movieInfos.Length, 1);
            Assert.AreEqual(movieInfos[0].Title, "title1");

            client = new RestClient($"https://localhost:44385/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44385/Movie/GetByIds?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(((JArray)movieDto.Data).Count, 0);
        }

        [Test]
        public void GetMoviesByPrefixTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44385/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44385/Movie/GetByPrefix?prefix=title");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.IsTrue(movieInfos.Length > 0);

            client = new RestClient($"https://localhost:44385/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44385/Movie/GetByPrefix?prefix=test");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(((JArray)movieDto.Data).Count, 1);
        }

        [Test]
        public void GetMoviesByAdvanceTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44385/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44385/Movie/GetByAdvanceSearch?title=title&director=&genre=&cast=&year=-1&award=&isOnShow=2");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.IsTrue(movieInfos.Length > 0);

            client = new RestClient($"https://localhost:44385/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);
        }

        [Test]
        public void GetMoviesAllTest()
        {
            var client = new RestClient("https://localhost:44385/Movie/GetMoviesAll");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + _token), ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.IsTrue(movieInfos.Length > 0);
        }
    }
}
