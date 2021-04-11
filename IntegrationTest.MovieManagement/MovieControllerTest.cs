using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            Assert.IsTrue(movieDto.Success);
            movieId = (int)movieDto.Data;

            client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);

            // Clean Up
            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);
        }

        [Test]
        public void UpdateMovieTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            Assert.IsTrue(movieDto.Success);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/Update?movieId={movieId}&title=The Party&director=Test Director&genre=Test Genre&cast=Test Cast&year=2022&award=Test Award&is_show=true");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            // Clean Up
            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);
        }

        [Test]
        public void DeleteMovieTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            Assert.IsTrue(movieDto.Success);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids=999999999");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByIdsTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/GetByIds?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(movieInfos.Length, 1);
            Assert.AreEqual(movieInfos[0].Title, "title1");

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44343/Movie/GetByIds?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
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
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/GetByPrefix?prefix=title");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.IsTrue(movieInfos.Length > 0);

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44343/Movie/GetByPrefix?prefix=test");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(((JArray)movieDto.Data).Count, 0);
        }

        [Test]
        public void GetMoviesByAdvanceTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/GetByAdvanceSearch?title=title&director=&genre=&cast=&year=-1&award=&isOnShow=2");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.IsTrue(movieInfos.Length > 0);

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);
        }

        [Test]
        public void GetMoviesAllTest()
        {
            var client = new RestClient("https://localhost:44343/Movie/GetMoviesAll");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.IsTrue(movieInfos.Length > 0);
        }
    }
}
