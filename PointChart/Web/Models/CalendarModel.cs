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
using AlwaysMoveForward.PointChart.BusinessLayer.Services;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class CalendarModel
    {
        public static string GenerateDateFilter(DateTime targetDate)
        {
            return targetDate.ToString("MM") + "/" + targetDate.ToString("dd") + "/" + targetDate.ToString("yyyy");
        }

        private DateTime viewDate;

        public CalendarModel(long chartId) : this(chartId, DateTime.Now){ }

        public CalendarModel(long chartId, DateTime viewDate)
        {
            this.ChartId = chartId;
            this.ViewDate = viewDate;
        }

        public long ChartId { get; set; }

        // Not sure I like this......Not sure how else to do it though
        public DateTime ViewDate 
        {
            get { return this.viewDate; }
            set
            {
                this.viewDate = value;
                this.CurrentMonth = CalendarMonthInfo.Create(this.ViewDate, this.ChartId);
                this.PreviousMonth = CalendarMonthInfo.Create(this.ViewDate.AddMonths(-1), this.ChartId);
                this.NextMonth = CalendarMonthInfo.Create(this.ViewDate.AddMonths(1), this.ChartId);
            }
        }
        
        public DateTime WeekStartDate { get; set; }
        public CalendarMonthInfo CurrentMonth { get; set; }
        public CalendarMonthInfo PreviousMonth { get; set; }
        public CalendarMonthInfo NextMonth { get; set; }
    }
}


