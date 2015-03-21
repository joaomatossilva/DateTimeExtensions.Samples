using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using DateTimeExtensions.Sample.Filters;
using DateTimeExtensions.Sample.LocalizedRouting;

namespace DateTimeExtensions.Sample {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapLocalizeRoute("Default",
                url: "{culture}/{controller}/{action}/{year}",
                defaults: new { controller = "Calendar", action = "Index", year = UrlParameter.Optional },
                constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" });

		    routes.MapRouteToLocalizeRedirect("RedirectToLocalize",
		        url: "{controller}/{action}/{year}",
                defaults: new { controller = "Calendar", action = "Index", year = UrlParameter.Optional }
            );

		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			GlobalFilters.Filters.Add(new CustomLocaleFilter());
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}