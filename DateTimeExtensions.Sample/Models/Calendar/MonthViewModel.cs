using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeExtensions.Sample.Models.Calendar
{
    public class MonthViewModel
    {
        public string Name { get; set; }
        public int StartDayOffset { get; set; }
        public IEnumerable<DayViewModel> Days { get; set; }
        public int EndDaysOffset { get; set; }
    }
}