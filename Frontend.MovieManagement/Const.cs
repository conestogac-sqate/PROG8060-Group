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

        public static string API_MOVIE_UPDATE = "/Movie/Update?movieId={0}&title={1}&director={2}&genre={3}&cast={4}&year={5}&award={6}&is_show={7}";

        public static string API_MOVIE_DELETE = "/Movie/Delete?ids={0}";

        public static string API_MOVIE_GET_ALL = "/Movie/GetMoviesAll";

        public static string API_MOVIE_GET_BY_IDs = "/Movie/GetByIds?ids={0}";

        public static string API_MOVIE_GET_BY_PREFIX = "/Movie/GetByPrefix?prefix={0}";

        public static string API_MOVIE_GET_ADVANCE = "/Movie/GetByAdvanceSearch?title={0}&director={1}&genre={2}&cast={3}&year={4}&award={5}&isOnShow={6}";

        public static string API_USER_GET_ALL = "/Session/GetUsersAll";

        public static string API_User_ADD = "/Session/AddUser?username={0}&password={1}&email={2}&canCreate={3}&canUpdate={4}&canRead={5}&canDelete={6}";

        public static string API_USER_UPDATE_ROLE = "/Session/UpdateUserRole?username={0}&canCreate={1}&canUpdate={2}&canRead={3}&canDelete={4}";
    }
}
