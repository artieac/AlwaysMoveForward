﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class TaskInput
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int MaxPerDay { get; set; }
    }
}