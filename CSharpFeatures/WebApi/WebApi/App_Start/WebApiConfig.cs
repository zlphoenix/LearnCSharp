using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                //constraints:new { controller= }
            );
            //config.Routes.MapHttpRoute(
            //    name: "FormApi",
            //    routeTemplate: "api/form/{vmId}/{id}",
            //    defaults: new { controller = "form", id = RouteParameter.Optional }
            //);
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

        }
    }
}
