using Newtonsoft.Json;
using NUnit.Framework;
using PROG8060_Group.Models;
using RestSharp;

namespace IntegrationTest.MovieManagement
{
    [TestFixture]
    public class MovieControllerTest
    {
        [Test]
        public void AddMovieTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title2&director=director2&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/Add?title=title2&director=director2&genre=genre&cast=cast1&year=2021");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void UpdateMovieTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/Update?movieId=17&title=title1sdfsdf&director=director1&genre=genre&cast=cast1&year=2021&award=award1dfs");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/Update?movieId=17&title=title1sdfsdf&director=director1&genre=genre&cast=cast1&year=2021");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void DeleteMovieTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/Delete?ids=17");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/Delete");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void MarkAsOnShowTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/MarkAsOnShow?ids=18");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/MarkAsOnShow");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByIdsTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/GetByIds?ids=4,5,6");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/GetByIds");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByPrefixTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/GetByPrefix?prefix=title");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/GetByPrefix");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByOnShowTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/GetByOnShow?isOnShow=false");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/GetByOnShow");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByPrefixOnShowTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/GetByPrefixOnShow?prefix=title&isOnShow=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/GetByPrefixOnShow");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesAllTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/GetMoviesAll");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/GetMoviesAll?param=1");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }
    }
}
