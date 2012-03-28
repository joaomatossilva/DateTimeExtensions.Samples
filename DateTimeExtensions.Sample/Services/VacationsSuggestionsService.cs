using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeExtensions.Sample.Services {
	public class VacationsSuggestionsService {
		private readonly DateTimeCultureInfo dateTimeCultureInfo;

		public VacationsSuggestionsService(DateTimeCultureInfo dateTimeCultureInfo) {
			this.dateTimeCultureInfo = dateTimeCultureInfo;
		}

		public IEnumerable<VacationPeriod> GetSuggestions(int year, int maxDaysForward) {
			DateTime date = new DateTime(year, 1, 1);
			List<VacationPeriod> results = new List<VacationPeriod>();
			DateTimeCultureInfo dateTimeCultureInfo = new DateTimeCultureInfo();
			do {
				results.AddRange(CrawlPeriod(dateTimeCultureInfo, date, maxDaysForward));
				date = date.AddDays(1);
			} while (date.Year <= year);

			return results.Where(s => s.Score < .50);			
		}

		private static IEnumerable<VacationPeriod> CrawlPeriod(DateTimeCultureInfo dateTimeCultureInfo, DateTime date, int maxDaysForward) {
			//Start crawling only if the last 2 days are working days
			if (date.IsWorkingDay(dateTimeCultureInfo) || !date.AddDays(-1).IsWorkingDay(dateTimeCultureInfo) || !date.AddDays(-2).IsWorkingDay(dateTimeCultureInfo)) {
				yield break;
			}
			int workingDaysCount = date.IsWorkingDay() ? 1 : 0;
			for (int i = 1; i <= maxDaysForward; i++) {
				DateTime endDate = date.AddDays(i);
				if (endDate.IsWorkingDay(dateTimeCultureInfo)) {
					workingDaysCount++;
				}
				if (workingDaysCount == 0) {
					continue;
				}
				//end crawl only when the next 2 days are working days
				if (endDate.IsWorkingDay(dateTimeCultureInfo) || !endDate.AddDays(1).IsWorkingDay(dateTimeCultureInfo) || !endDate.AddDays(2).IsWorkingDay(dateTimeCultureInfo)) {
					continue;
				}
				yield return new VacationPeriod { StartDate = date, EndDate = endDate, WorkingDays = workingDaysCount };
			}
		}
	}

	public class VacationPeriod {
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int WorkingDays { get; set; }
		public int TotalDays {
			get {
				return EndDate.Subtract(StartDate).Days +1; //the plus one is the end day inclusive
			}
		}
		public float Score {
			get {
				return (WorkingDays / (float)TotalDays);
			}
		}
	}
}