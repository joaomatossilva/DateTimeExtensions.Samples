using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DateTimeExtensions.Sample.Models;

namespace DateTimeExtensions.Sample.Controllers {
	public class LocaleController : Controller {
		public ActionResult Index() {
			var localeName = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
			ViewBag.Locale = localeName;
			return PartialView();
		}

		public ActionResult SelectLocale() {
			var locales = new List<SelectLocaleViewModel> {
				new SelectLocaleViewModel { LocaleName = "en-US", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "en-GB", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "pt-PT", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "pt-BR", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "fr-FR", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "de-DE", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "es-ES", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "da-DK", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "fi-FI", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "is-IS", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "nb-NO", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "nl-NL", HasHolidays = true, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "nl-BE", HasHolidays = false, HasNaturalText = true },
				new SelectLocaleViewModel { LocaleName = "sv-SE", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "es-AR", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "es-MX", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "en-AU", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "en-ZA", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "fr-CA", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "ar-SA", HasHolidays = true },
				new SelectLocaleViewModel { LocaleName = "it-IT", HasHolidays = true },
                new SelectLocaleViewModel { LocaleName = "en-NZ", HasHolidays = true }
			};
			return PartialView(locales);
		}

		public ActionResult SetLocale(string localeName) {
			Session["localeName"] = localeName;
			return this.Redirect("/");
		}
	}
}
