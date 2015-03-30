using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    /// <summary>
    /// A chart task
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Initializes an instance of a Task
        /// </summary>
        public Task()
        {
            this.Id = -1;
        }

        /// <summary>
        /// Gets and sets the identifier of the instance
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets and sets the name of the chart
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets and sets the Points the task is worth
        /// </summary>
        public double Points { get; set; }

        /// <summary>
        /// Gets and sets the maximum number of times this task can be done.
        /// </summary>
        public int MaxAllowedDaily { get; set; }

        /// <summary>
        /// Gets and sets the administrator id for this task.
        /// </summary>
        public long CreatorId { get; set; }

        public IList<CompletedTask> CompletedTasks { get; set; }
    }
}
