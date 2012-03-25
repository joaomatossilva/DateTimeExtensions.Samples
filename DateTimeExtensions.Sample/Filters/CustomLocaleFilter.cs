using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DateTimeExtensions.Sample.Filters {
	public class CustomLocaleFilter : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			HttpSessionStateBase session = filterContext.HttpContext.Session;
			if (!string.IsNullOrEmpty((string)session["localeName"])) {
				System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo((string)session["localeName"]);
			}
			base.OnActionExecuting(filterContext);
		}
	}
}