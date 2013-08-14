using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.Entities
{
    public class ChartTaskInstance
    {
        public ChartTaskInstance()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public int ChartId { get; set; }
        public int TaskId { get; set; }
        public DateTime DateCompleted { get; set; }
        public int NumberOfTimesCompleted { get; set; }
        public int AdministratorId { get; set; }
    }
}
