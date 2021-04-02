using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.MovieManagement
{
    public class Const
    {
        public static string API_HOST = "https://localhost:44343";

        public static string API_LOGIN = "/Session/Login?username={0}&password={1}";

        public static string API_LOGOUT = "/Session/Logout?username={0}";

        public static string API_MOVIE_ADD = "/Movie/Add?title={0}&director={1}&genre={2}&cast={3}&year={4}&award={5}";

        public static string API_MOVIE_GET_ALL = "/Movie/GetMoviesAll";
    }
}
