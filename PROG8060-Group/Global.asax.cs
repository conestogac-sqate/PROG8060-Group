using PROG8060_Group.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PROG8060_Group
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            MovieConfig.Initialize();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
