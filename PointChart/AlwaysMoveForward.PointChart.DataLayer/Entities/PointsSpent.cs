﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.Entities
{
    public class PointsSpent
    {
        public PointsSpent()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public DateTime DateSpent { get; set; }
        public double Amount { get; set; }
        public String Description { get; set; }
        public PointEarner PointEarner { get; set; }
    }
}
