using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Reflection;


namespace TradeRepo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //var ConstraintResolver = new DefaultInlineConstraintResolver();
            //ConstraintResolver.ConstraintMap.Add("enum",typeof(EnumerationConstraint));
            //config.MapHttpAttributeRoutes(ConstraintResolver);
            
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/repo/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }

    
}
