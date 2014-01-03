using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using DateTimeExtensions.Sample.Areas.Api.Models;
using DateTimeExtensions.WorkingDays;

namespace DateTimeExtensions.Sample.Areas.Api.Controllers {
	public class HolidayObservancesController : ApiController {

        public IEnumerable<HolidayObservance> Get(string locale, int year, string language)
        {
		    if (!string.IsNullOrEmpty(language))
		    {
		        System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
		    }
			var workingdayCultureInfo = new WorkingDayCultureInfo(locale);
			var holidays = workingdayCultureInfo.Holidays;
			var observances =
				holidays.Select(
					o =>
					new HolidayObservance {
						Name = o.Name,
						ObservanceDate = o.GetInstance(year)
					})
                    .Where(h => h.ObservanceDate.HasValue)
                    .OrderBy(h => h.ObservanceDate);
			return observances;
		}

	}
}
