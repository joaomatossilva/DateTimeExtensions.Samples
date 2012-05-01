using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using DateTimeExtensions.Sample.Areas.Api.Models;

namespace DateTimeExtensions.Sample.Areas.Api.Controllers {
	public class HolidayObservancesController : ApiController {

		public IEnumerable<HolidayObservance> Get(string locale, int year) {
			var dateTimeCultureInfo = new DateTimeCultureInfo(locale);
			var holidays = dateTimeCultureInfo.Holidays;
			var observances =
				holidays.Select(
					o =>
					new HolidayObservance {
					                      	Name = o.Name,
					                      	ObservanceDate = o.GetInstance(year).HasValue ? o.GetInstance(year).Value.ToShortDateString() : "(no observance)"
					                      });
			return observances;
		}

	}
}
