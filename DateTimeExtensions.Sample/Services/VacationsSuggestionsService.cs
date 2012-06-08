using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DateTimeExtensions.WorkingDays;

namespace DateTimeExtensions.Sample.Services {
	public class VacationsSuggestionsService {

		public IEnumerable<VacationPeriod> GetSuggestions(int year, int maxDaysForward) {
			DateTime date = new DateTime(year, 1, 1);
			List<VacationPeriod> results = new List<VacationPeriod>();
			WorkingDayCultureInfo workingdayCultureInfo = new WorkingDayCultureInfo();
			do {
				results.AddRange(CrawlPeriod(workingdayCultureInfo, date, maxDaysForward));
				date = date.AddDays(1);
			} while (date.Year <= year);

			return results.Where(s => s.Score < .50);			
		}

		private static IEnumerable<VacationPeriod> CrawlPeriod(WorkingDayCultureInfo workingdayCultureInfo, DateTime date, int maxDaysForward)
		{
			//Start crawling only if the last 2 days are working days
			if (date.IsWorkingDay(workingdayCultureInfo) || !date.AddDays(-1).IsWorkingDay(workingdayCultureInfo) || !date.AddDays(-2).IsWorkingDay(workingdayCultureInfo)) {
				yield break;
			}
			int workingDaysCount = date.IsWorkingDay() ? 1 : 0;
			for (int i = 1; i <= maxDaysForward; i++) {
				DateTime endDate = date.AddDays(i);
				if (endDate.IsWorkingDay(workingdayCultureInfo)) {
					workingDaysCount++;
				}
				if (workingDaysCount == 0) {
					continue;
				}
				//end crawl only when the next 2 days are working days
				if (endDate.IsWorkingDay(workingdayCultureInfo) || !endDate.AddDays(1).IsWorkingDay(workingdayCultureInfo) || !endDate.AddDays(2).IsWorkingDay(workingdayCultureInfo)) {
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