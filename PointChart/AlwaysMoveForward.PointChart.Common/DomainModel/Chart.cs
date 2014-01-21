using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    public class Chart
    {
        public Chart()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public int AdministratorId { get; set; }
        public IList<Task> Tasks { get; set; }
        public IList<CompletedTask> CompletedTasks { get; set; } 
    }
}
