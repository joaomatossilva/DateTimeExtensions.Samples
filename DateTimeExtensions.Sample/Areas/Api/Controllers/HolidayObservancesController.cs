using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using DateTimeExtensions.Sample.Areas.Api.Models;
using DateTimeExtensions.WorkingDays;

namespace DateTimeExtensions.Sample.Areas.Api.Controllers {
	public class HolidayObservancesController : ApiController {

		public IEnumerable<HolidayObservance> Get(string locale, int year) {
			var workingdayCultureInfo = new WorkingDayCultureInfo(locale);
			var holidays = workingdayCultureInfo.Holidays;
			var observances =
				holidays.Select(
					o =>
					new HolidayObservance {
						Name = o.Name,
						ObservanceDate = o.GetInstance(year)
					});
			return observances;
		}

	}
}
