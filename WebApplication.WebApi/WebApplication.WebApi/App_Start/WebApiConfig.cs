using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using WebApplication.WebApi.Filter;
using RouteParameter = System.Web.Http.RouteParameter;

namespace WebApplication.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(new EnableCorsAttribute("*", "*","GET,PUT,POST")
            {
                SupportsCredentials = true
            });
            config.Filters.Add(new CheckActionFilter());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Filters.Add(new LogFilter());
        }
    }
}
