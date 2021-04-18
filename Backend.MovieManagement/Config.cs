using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.MovieManagement
{
    public class Config
    {
        public static string Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
        public static string DBConnectionUrl = "server=prog8050-mysql.cdcfsbhowqh3.us-west-2.rds.amazonaws.com;uid=admin;pwd=Password;database=dbMovie";
    }
}
