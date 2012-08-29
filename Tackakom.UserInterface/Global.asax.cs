using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Tackakom.UserInterface
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ByDate", // Route name
                "date/{date}/{page}", // URL with parameters
                new { controller = "Event", action = "GetEventsByDate", Date = UrlParameter.Optional, page = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "SingleEvent", // Route name
                "single/{Id}/", // URL with parameters
                new { controller = "Event", action = "SingleEvent", Id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "HostList", // Route name
                "places/{hostId}/{page}", // URL with parameters
                new { controller = "Host", action = "Index", hostId = UrlParameter.Optional, page = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "EventList", // Route name
                "events/{page}", // URL with parameters
                new { controller = "Event", action = "Index", page = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
              "Editing", // Route name
              "editing/{page}", // URL with parameters
              new { controller = "Event", action = "Editing", page = UrlParameter.Optional } // Parameter defaults
          );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
        }
    }
}