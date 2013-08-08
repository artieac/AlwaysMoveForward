/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.PointChart.BusinessLayer.Service;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class CalendarModel
    {
        public static string GenerateDateFilter(DateTime targetDate)
        {
            return targetDate.ToString("MM") + "-" + targetDate.ToString("dd") + "-" + targetDate.ToString("yyyy");
        }

        public CalendarModel()
        {
            this.TargetMonth = DateTime.Now;
        }

        public String GenerateUrlForDay(DateTime startDate)
        {
            return RouteInformation + "?targetDate=" + CalendarModel.GenerateDateFilter(startDate);
        }

        public String GenerateUrlForMonth(int offset)
        {
            return RouteInformation + "?targetDate=" + CalendarModel.GenerateDateFilter(this.TargetMonth.AddMonths(offset));
        }

        // Not sure I like this......Not sure how else to do it though
        public String RouteInformation { get; set; }
        public DateTime ViewDate { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime TargetMonth { get; set; }
    }
}


