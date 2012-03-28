using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeExtensions.Sample.Models {
	public class VacationsSuggestionsViewModel {
		public int TotalDaysOff { get; set; }
		public IEnumerable<VacationSuggestion> Suggestions { get; set; }
	}

	public class VacationSuggestion {
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int VacationDaysSpent { get; set; }
	}
}