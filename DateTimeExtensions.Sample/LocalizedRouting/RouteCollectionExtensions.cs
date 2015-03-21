using System.Web.Routing;

namespace DateTimeExtensions.Sample.LocalizedRouting
{
    public static class RouteCollectionExtensions
    {
        public static Route MapRouteToLocalizeRedirect(this RouteCollection routes, string name, string url, object defaults)
        {
            var redirectRoute = new Route(url, new RouteValueDictionary(defaults), new LocalizationRedirectRouteHandler());
            routes.Add(name, redirectRoute);

            return redirectRoute;
        }

        public static Route MapLocalizeRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapLocalizeRoute(name, url, defaults, new { });
        }

        public static Route MapLocalizeRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            var route = new Route(
                url,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints),
                new LocalizedRouteHandler());

            routes.Add(name, route);

            return route;
        }
    }
}