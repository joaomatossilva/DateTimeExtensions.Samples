using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DateTimeExtensions.Sample.Models.Calendar;
using System.Globalization;
using DateTimeExtensions.Sample.Services;
using DateTimeExtensions.Sample.Models;
using DateTimeExtensions.Export;
using DateTimeExtensions.WorkingDays;

namespace DateTimeExtensions.Sample.Controllers {
	public class CalendarController : Controller {
	    readonly WorkingDayCultureInfo workingdayCultureInfo = new WorkingDayCultureInfo();
		private static readonly Calendar GregorianCalendar = new GregorianCalendar();
		//
		// GET: /Calendar/

		public ActionResult Index(int? year) {
		    if (year == null)
		    {
                return RedirectToAction("Index", new { year = DateTime.Today.Year });
		    }

			var years = new List<int>();
			for (int i = 0; i < 5; i++) {
				years.Add(DateTime.Now.Year - 2 + i);
			}
			if (!years.Contains((int)year)) {
				years.Add((int)year);
			}
			ViewBag.Locale = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
			ViewBag.CurrentYear = year;
			ViewBag.Years = years;
			return View();
		}

        [HttpPost]
	    public ActionResult Index(int year)
	    {
            return RedirectToAction("Index", new { year });
	    }

	    public ActionResult YearCalendar(int year) {
			var yearViewModel = new YearViewModel {
				YearName = year.ToString(),
				Months = BuildMonthsViewModel(year)
			};
			return PartialView(yearViewModel);
		}

		public ActionResult VacationsSuggestions(int year) {
			var vacationsService = new VacationsSuggestionsService();
			var suggestionsViewModel = vacationsService.GetSuggestions(year, 16)
				.GroupBy(s => s.TotalDays)
				.Select(t => new VacationsSuggestionsViewModel {
					TotalDaysOff = t.Key,
					Suggestions = t.OrderBy(p => p.Score)
						.Select( p=> new VacationSuggestion {
							StartDate = p.StartDate,
							EndDate = p.EndDate,
							VacationDaysSpent = p.WorkingDays
						})
				})
				.OrderByDescending(t => t.TotalDaysOff);
			return PartialView(suggestionsViewModel);
		}

		public ActionResult ExportToFile(int year) {
			var holFormat = ExportHolidayFormatLocator.LocateByType(ExportType.OfficeHolidays);
			var memoryStream = new MemoryStream();
			var writer = new StreamWriter(memoryStream);
            holFormat.Export(new WorkingDayCultureInfo(), year, writer);
			writer.Flush();
			memoryStream.Position = 0;
			return File(memoryStream, "text/plain", "outlook.hol");
		}

		private IEnumerable<MonthViewModel> BuildMonthsViewModel(int year) {
			for (int i = 1; i <= 12; i++) {
				var startMonthDate = new DateTime(year, i, 1);
				yield return new MonthViewModel {
					Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
					StartDayOffset = (int)startMonthDate.DayOfWeek,
					Days = BuildDaysForMonthViewModel(year, i),
					EndDaysOffset = 6 - (int)startMonthDate.LastDayOfTheMonth().DayOfWeek
				};
			}
		}

		private IEnumerable<DayViewModel> BuildDaysForMonthViewModel(int year, int month) {
			var daysInMonth = GregorianCalendar.GetDaysInMonth(year, month);
			for (int i = 1; i <= daysInMonth; i++) {
				var day = new DateTime(year, month, i);
				yield return new DayViewModel {
					DayText = i.ToString(),
					IsWorkingDay = day.IsWorkingDay(workingdayCultureInfo),
					IsHoliday = day.IsHoliday(workingdayCultureInfo)
				};
			}
		}
	}
}
