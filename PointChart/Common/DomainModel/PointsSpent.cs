using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    /// <summary>
    /// Represent a points spent event
    /// </summary>
    public class PointsSpent
    {
        /// <summary>
        /// Initializes an instance of the Points Spent class.
        /// </summary>
        public PointsSpent()
        {
            this.Id = -1;
        }

        /// <summary>
        /// Gets and sets the identifier for the instance
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets and sets the date the points were spent on
        /// </summary>
        public DateTime DateSpent { get; set; }

        /// <summary>
        /// Gets and sets the amount of points that were spent
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets and sets the description of how the points were spent
        /// </summary>
        public string Description { get; set; }

        public long PointEarnerId { get; set; }
    }
}
