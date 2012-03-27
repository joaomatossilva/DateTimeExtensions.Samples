using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeExtensions.Sample.Models.Calendar
{
    public class DayViewModel
    {        
        public string DayText { get; set; }
        public bool IsWorkingDay { get; set; }
        public bool IsHoliday { get; set; }
    }
}