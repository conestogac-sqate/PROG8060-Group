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
    }
}
