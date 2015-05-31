using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    /// <summary>
    /// A point chart owned by a Point Earner
    /// </summary>
    public class Chart
    {
        /// <summary>
        /// Initialize a new instance of a Chart.
        /// </summary>
        public Chart()
        {
            this.Id = -1;
        }

        /// <summary>
        /// Gets and sets the unique identifier of the chart
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Gets and sets name of the chart
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets and sets the identifier of the person that adminsters this chart/point earner
        /// </summary>
        public long CreatorId { get; set; }

        /// <summary>
        /// Gets and sets the identifier of the person that adminsters this chart/point earner
        /// </summary>
        public PointChartUser PointEarner { get; set; }

        /// <summary>
        /// Gets and sets all the tasks on the chart
        /// </summary>
        public IList<Task> Tasks { get; set; }
        
        public Task GetTask(long taskId)
        {
            return this.Tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public bool IsCreator(PointChartUser user)
        {
            bool retVal = false;

            if(user != null)
            {
                if(this.CreatorId == user.Id)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public bool IsPointEarner(PointChartUser user)
        {
            bool retVal = false;

            if (user != null)
            {
                if (this.PointEarner.Id == user.Id)
                {
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}
