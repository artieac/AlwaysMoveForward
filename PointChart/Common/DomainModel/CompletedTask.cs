using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    /// <summary>
    /// A completed task
    /// </summary>
    public class CompletedTask
    {
        /// <summary>
        /// Initializes an instance of a completed task
        /// </summary>
        public CompletedTask()
        {
            this.Id = -1;
        }

        /// <summary>
        /// Gets and sets the identifier for the completed task
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets and sets the Chart that was completed.
        /// </summary>
        public long ChartId { get; set; }

        /// <summary>
        /// Gets and sets the Task that was completed.
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Gets and sets how many points the task was worth at the time of completion
        /// </summary>
        public double PointValue { get; set; }

        /// <summary>
        /// Gets and sets the date the task was completed
        /// </summary>
        public DateTime DateCompleted { get; set; }

        /// <summary>
        /// Gets and sets the number of times the task was completed
        /// </summary>
        public int NumberOfTimesCompleted { get; set; }
    }
}
