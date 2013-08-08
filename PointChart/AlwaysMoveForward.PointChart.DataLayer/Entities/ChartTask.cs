using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.Entities
{
    public class ChartTask
    {
        public ChartTask()
        {
            this.Id = -1;
        }

        public int Id { get; set;}
        public int ChartId{ get; set;}
        public int TaskId { get; set;}
    }
}
