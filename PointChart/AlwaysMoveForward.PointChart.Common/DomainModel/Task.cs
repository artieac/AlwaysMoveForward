using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    public class Task
    {
        public Task()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public double Points { get; set; }
        public int MaxAllowedDaily { get; set; }
        public int AdministratorId { get; set; }
    }
}
