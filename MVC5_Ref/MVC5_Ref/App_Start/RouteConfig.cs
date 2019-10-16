using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5_Ref
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //this will have to be removed eventually
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute("404-NotFound", "PageNotFound", new { controller = "Base", action = "NotFound" });

            routes.MapRoute("CatchAll", "{*url}", new { controller = "Base", action = "PageNotFound" });
        }
    }
}
