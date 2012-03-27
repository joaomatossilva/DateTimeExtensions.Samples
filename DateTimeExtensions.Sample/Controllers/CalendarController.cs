﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DateTimeExtensions.Sample.Models.Calendar;
using System.Globalization;

namespace DateTimeExtensions.Sample.Controllers {
	public class CalendarController : Controller {
		DateTimeCultureInfo dateTimeCultureInfo = new DateTimeCultureInfo();
		//
		// GET: /Calendar/

		public ActionResult Index(int year) {
			var years = new List<int>();
			for (int i = 0; i < 5; i++) {
				years.Add(DateTime.Now.Year - 2 + i);
			}
			if (!years.Contains(year)) {
				years.Add(year);
			}
			ViewBag.CurrentYear = year;
			ViewBag.Years = years;
			return View();
		}

		public ActionResult YearCalendar(int year) {
			var yearViewModel = new YearViewModel {
				YearName = year.ToString(),
				Months = BuildMonthsViewModel(year)
			};
			return PartialView(yearViewModel);
		}

		public ActionResult HolidaysList(int year) {
			var holidays = dateTimeCultureInfo.GetHolidaysOfYear(year);
			var holidaysInstances = holidays.Select(h => new { date = h.GetInstance(year).HasValue ? h.GetInstance(year).Value.ToShortDateString() : null, holiday = h })
				.ToDictionary(x => x.holiday, x => x.date);
			return PartialView(holidaysInstances);
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
			var daysInMonth = CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(year, month);
			for (int i = 1; i <= daysInMonth; i++) {
				var day = new DateTime(year, month, i);
				yield return new DayViewModel {
					DayText = i.ToString(),
					IsWorkingDay = dateTimeCultureInfo.IsWorkingDay(day.DayOfWeek),
					IsHoliday = dateTimeCultureInfo.GetHolidaysOfYear(year).Where(h => h.IsInstanceOf(day)).SingleOrDefault() != null
				};
			}
		}
	}
}
