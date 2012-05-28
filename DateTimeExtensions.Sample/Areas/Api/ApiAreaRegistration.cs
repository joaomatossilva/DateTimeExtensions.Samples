using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;

namespace DateTimeExtensions.Sample.Areas.Api {
	public class ApiAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "Api";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.Routes.MapHttpRoute(
				"CalendarApi",
				"Api/{controller}/{locale}/{year}",
				new {  }
			);

			context.Routes.MapHttpRoute(
				"BigDayApi",
				"Api/{controller}",
				new { }
			);
		}
	}
}
