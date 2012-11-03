using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DateTimeExtensions.Common;
using DateTimeExtensions.NaturalText;
using DateTimeExtensions.Sample.Models;
using DateTimeExtensions.WorkingDays;

namespace DateTimeExtensions.Sample.Controllers {
	public class LocaleController : Controller {
		public ActionResult Index() {
			var localeName = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
			ViewBag.Locale = localeName;
			return PartialView();
		}

		public ActionResult SelectLocale()
		{
		    var locales = DiscoverLocales();
			return PartialView(locales);
		}

		public ActionResult SetLocale(string localeName) {
			Session["localeName"] = localeName;
			return this.Redirect("/");
		}

        private IEnumerable<SelectLocaleViewModel> DiscoverLocales()
        {
            var holidaysLocales = new List<string>();
            var naturalTimeLocales = new List<string>();
            var holidaysType = typeof (IHolidayStrategy);
            var naturalTimeType = typeof (INaturalTimeStrategy);
            var assemblyTypes = holidaysType.Assembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                if (holidaysType.IsAssignableFrom(type))
                {
                    holidaysLocales.AddRange(type.GetCustomAttributes(false).Select( l => ((LocaleAttribute)l).Locale));
                }
                if (naturalTimeType.IsAssignableFrom(type))
                {
                    naturalTimeLocales.AddRange(type.GetCustomAttributes(false).Select(l => ((LocaleAttribute)l).Locale));
                }
            }

            var locales = from h in holidaysLocales
                          join n in naturalTimeLocales on h equals n into nh
                          from subN in nh.DefaultIfEmpty()
                          select new SelectLocaleViewModel
                                     {
                                         LocaleName = h,
                                         HasHolidays = h != null,
                                         HasNaturalText = subN != null
                                     };
            return locales.OrderBy(l => l.LocaleName);
        } 
	}
}
