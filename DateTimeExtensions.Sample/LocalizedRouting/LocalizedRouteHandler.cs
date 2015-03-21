using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DateTimeExtensions.Sample.LocalizedRouting
{
    public class LocalizedRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var newLocale = requestContext.HttpContext.Request["changeCulture"];
            var urlLocale = requestContext.RouteData.Values["culture"] as string;
            var cultureName = urlLocale ?? "";

            if (!string.IsNullOrEmpty(newLocale))
            {
                var routeValues = requestContext.RouteData.Values;
                routeValues["culture"] = newLocale;
                requestContext.HttpContext.Response.AppendCookie(new HttpCookie("locale", newLocale));
                return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
            }

            if (string.IsNullOrEmpty(cultureName))
            {
                return GetDefaultLocaleRedirectHandler(requestContext);
            }

            try
            {
                var culture = CultureInfo.GetCultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                // if CultureInfo.GetCultureInfo throws exception
                // we should redirect with default locale
                return GetDefaultLocaleRedirectHandler(requestContext);
            }

            requestContext.HttpContext.Response.AppendCookie(new HttpCookie("locale", cultureName));
            return base.GetHttpHandler(requestContext);
        }

        private static IHttpHandler GetDefaultLocaleRedirectHandler(RequestContext requestContext)
        {
            var uiCulture = CultureInfo.CurrentUICulture;
            var routeValues = requestContext.RouteData.Values;
            routeValues["culture"] = uiCulture.Name;
            return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
        }
    }
}