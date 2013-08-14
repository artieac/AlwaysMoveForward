﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.Entities
{
    public class CompletedTask
    {
        public CompletedTask()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public Chart Chart { get; set; }
        public Task Task { get; set; }
        public DateTime DateCompleted { get; set; }
        public int NumberOfTimesCompleted { get; set; }
    }
}
