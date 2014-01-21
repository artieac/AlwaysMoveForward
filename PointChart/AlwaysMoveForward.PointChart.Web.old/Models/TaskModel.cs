using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class TaskModel
    {
        public IList<Task> Tasks { get; set; }
    }
}