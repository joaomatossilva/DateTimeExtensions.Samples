using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DateTimeExtensions.Sample.Models;
using DateTimeExtensions;

namespace DateTimeExtensions.Sample.Controllers {
	public class HowLongController : Controller {
		//
		// GET: /HowLong/

		public ActionResult Index() {
			return View();
		}

		public ActionResult ToChristmas() {
			var model = new HowLongToViewModel();
			DateTime startDate = DateTime.Now;
			DateTime christmas = ChristianHolidays.Christmas.GetInstance(startDate.Year).Value;
			model.HowLongHuman = startDate.ToNaturalText(christmas, false);
			model.HowLongHumanRounded = startDate.ToNaturalText(christmas, true);
			model.HowLongExact = startDate.ToExactNaturalText(christmas);
			return PartialView("HowLongTo",model);
		}

		public ActionResult ToMyBirthday(DateTime myBirthday) {
			var model = new HowLongToViewModel();
			DateTime startDate = DateTime.Now;
			model.HowLongHuman = startDate.ToNaturalText(myBirthday, false);
			model.HowLongHumanRounded = startDate.ToNaturalText(myBirthday, true);
			model.HowLongExact = startDate.ToExactNaturalText(myBirthday);
			return PartialView("HowLongTo", model);
		}
	}
}
