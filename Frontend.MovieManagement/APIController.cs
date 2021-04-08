using Newtonsoft.Json;
using PROG8060_Group.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.MovieManagement
{
    public class APIController
    {
        public static ApiResult RequestLogin(string username, string password)
        {
            string url = string.Format(Const.API_HOST + Const.API_LOGIN, username, password);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<UserInfo>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestAddMovie(MovieInfo movieInfo)
        {
            string url = string.Format(Const.API_HOST + Const.API_MOVIE_ADD, movieInfo.Title, movieInfo.Director, movieInfo.Genere, movieInfo.Cast, movieInfo.Year, movieInfo.Award);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestEditMovie(MovieInfo movieInfo)
        {
            string url = string.Format(Const.API_HOST + Const.API_MOVIE_UPDATE, movieInfo.Id, movieInfo.Title, movieInfo.Director, movieInfo.Genere, movieInfo.Cast, movieInfo.Year, movieInfo.Award, movieInfo.IsOnShow);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestGetAllMovies()
        {
            string url = string.Format(Const.API_HOST + Const.API_MOVIE_GET_ALL);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestGetMoviesByIds(string id)
        {
            string url = string.Format(Const.API_HOST + Const.API_MOVIE_GET_BY_IDs, id);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestGetMoviesByPrefix(string prefix)
        {
            string url = string.Format(Const.API_HOST + Const.API_MOVIE_GET_BY_PREFIX, prefix);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestGetMoviesAdvance(SearchConfiguration searchConfiguration)
        {
            string url = string.Format(Const.API_HOST + Const.API_MOVIE_GET_ADVANCE, searchConfiguration.Title, searchConfiguration.Director, searchConfiguration.Genre, searchConfiguration.Cast, searchConfiguration.Year, searchConfiguration.Award, (int)searchConfiguration.IsOnShow);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<MovieInfo[]>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestDeleteMovie(string id)
        {
            string url = string.Format(Const.API_HOST + Const.API_MOVIE_DELETE, id);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<bool>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestGetAllUsers()
        {
            string url = string.Format(Const.API_HOST + Const.API_USER_GET_ALL);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<UserInfo[]>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestAddUser(UserInfo userInfo)
        {
            string url = string.Format(Const.API_HOST + Const.API_User_ADD, userInfo.Name, userInfo.Password, userInfo.Email, userInfo.CanCreate, userInfo.CanUpdate, userInfo.CanRead, userInfo.CanDelete);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }

        public static ApiResult RequestEditUserRole(UserInfo userInfo)
        {
            string url = string.Format(Const.API_HOST + Const.API_USER_UPDATE_ROLE, userInfo.Name, userInfo.CanCreate, userInfo.CanUpdate, userInfo.CanRead, userInfo.CanDelete);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            IRestResponse response = client.Execute(request);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                return JsonConvert.DeserializeObject<ApiSuccess<int>>(response.Content);
            }
            catch
            {
                return JsonConvert.DeserializeObject<ApiError>(response.Content);
            }
        }
    }
}
