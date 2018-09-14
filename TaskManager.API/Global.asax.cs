using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace TaskManager.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Stop IIS/Asp.Net breaking our routes
            //RouteTable.Routes.RouteExistingFiles = true;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
