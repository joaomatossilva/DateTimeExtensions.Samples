using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using DateTimeExtensions.NaturalText;

namespace DateTimeExtensions.Sample.Areas.Api.Controllers {
	public class TheBigDayController : ApiController {
		public string Get() {
			NaturalTextCultureInfo ptCultureInfo = new NaturalTextCultureInfo("pt-PT");
			var now = DateTime.Now;
			return now.ToNaturalText(new DateTime(2012, 6, 16), true, ptCultureInfo);
		}
	}
}
