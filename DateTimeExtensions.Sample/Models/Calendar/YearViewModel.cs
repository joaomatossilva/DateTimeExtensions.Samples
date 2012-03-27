using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeExtensions.Sample.Models.Calendar
{
    public class YearViewModel
    {
        public string YearName { get; set; }
        public IEnumerable<MonthViewModel> Months { get; set; }
    }
}