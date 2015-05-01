using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class CalendarMonthInfo
    {
        public static CalendarMonthInfo Create(DateTime date, long chartId)
        {
            CalendarMonthInfo retVal = new CalendarMonthInfo();
            retVal.Month = date;
            retVal.Name = retVal.Month.ToString("MMMM");
            retVal.Url = "/api/Chart/" + chartId + "/" + CalendarModel.GenerateDateFilter(retVal.Month);
            retVal.WeekStartDate = AlwaysMoveForward.Common.Utilities.Utils.DetermineStartOfWeek(retVal.Month.AddDays(-(retVal.Month.Day - 1)));
            return retVal;
        }

        public string Name { get; set; }
        public DateTime Month { get; set; }
        public String Url { get; set; }
        public DateTime WeekStartDate { get; set; }
    }
}