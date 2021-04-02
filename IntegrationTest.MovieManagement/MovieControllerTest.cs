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
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            Assert.IsTrue(movieDto.Success);
            movieId = (int)movieDto.Data;

            client = new RestClient("https://localhost:44343/Movie/Add?title=titleAdd&director=directorAdd&genre=genreAdd&cast=castAdd&year=2021&award=awardAdd");
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
            var client = new RestClient("https://localhost:44343/Movie/Add?title=titleAdd&director=directorAdd&genre=genreAdd&cast=castAdd&year=2021&award=awardAdd");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/Update?movieId={movieId}&title=titleUpdate&director=directorUpdate&genre=genreUpdate&cast=castUpdate&year=2020&award=awardUpdate");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            // Prerequisite - Fail
            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44343/Movie/Update?movieId={movieId}&title=titleUpdate&director=directorUpdate&genre=genreUpdate&cast=castUpdate&year=2020&award=awardUpdate");
            client.Timeout = -1;
            request = new RestRequest(Method.PUT);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void DeleteMovieTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=titleAdd&director=directorAdd&genre=genreAdd&cast=castAdd&year=2021&award=awardAdd");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void MarkAsOnShowTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/MarkAsOnShow?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient($"https://localhost:44343/Movie/MarkAsOnShow?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
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

            // Prerequisite - Fail
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
            Assert.IsFalse(movieDto.Success);
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
            Assert.AreEqual(movieInfos[0].Title, "title1");

            client = new RestClient($"https://localhost:44343/Movie/GetByPrefix?prefix=test");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByOnShowTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/GetByOnShow?isOnShow=false");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(movieInfos[0].Title, "title1");

            client = new RestClient($"https://localhost:44343/Movie/GetByOnShow?isOnShow=true");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesByPrefixOnShowTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient($"https://localhost:44343/Movie/GetByPrefixOnShow?prefix=title&isOnShow=false");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(movieInfos[0].Title, "title1");

            client = new RestClient($"https://localhost:44343/Movie/GetByPrefixOnShow?prefix=test&isOnShow=false");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }

        [Test]
        public void GetMoviesAllTest()
        {
            // Prerequisite - Pass
            int movieId = -1;
            var client = new RestClient("https://localhost:44343/Movie/Add?title=title1&director=director1&genre=genre&cast=cast1&year=2021&award=award1");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ApiResult movieDto = JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            movieId = (int)movieDto.Data;

            client = new RestClient("https://localhost:44343/Movie/GetMoviesAll");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            MovieInfo[] movieInfos = (MovieInfo[])movieDto.Data;
            Assert.IsTrue(movieDto.Success);
            Assert.AreEqual(movieInfos[0].Title, "title1");

            // Prerequisite - Fail
            client = new RestClient($"https://localhost:44343/Movie/Delete?ids={movieId}");
            client.Timeout = -1;
            request = new RestRequest(Method.DELETE);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            Assert.IsTrue(movieDto.Success);

            client = new RestClient("https://localhost:44343/Movie/GetMoviesAll");
            client.Timeout = -1;
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            movieDto = JsonConvert.DeserializeObject<ApiError>(response.Content);
            Assert.IsFalse(movieDto.Success);
        }
    }
}
