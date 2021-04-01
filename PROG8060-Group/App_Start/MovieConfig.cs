using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG8060_Group.App_Start
{
    public class MovieConfig
    {
        public static string DBConnectionUrl = "server=prog8050-mysql.cdcfsbhowqh3.us-west-2.rds.amazonaws.com;uid=admin;pwd=Password;database=dbMovie";

        public static string APIKey = "";

        public static DateTime ExpiredTime = DateTime.MinValue;

        public static void Initialize()
        {

        }
    }
}