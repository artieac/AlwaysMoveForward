using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    /// <summary>
    /// This class represents someone who earns points by doing tasks in a chart
    /// </summary>
    public class PointEarner
    {
        /// <summary>
        /// Keep a calculated value of points spent
        /// </summary>
        private double pointsSpent;

        /// <summary>
        /// Initializes an instance of the Point Earner class.
        /// </summary>
        public PointEarner()
        {
            this.pointsSpent = -1;
            this.Id = -1;
        }

        /// <summary>
        /// Gets and sets the identifier for the point earner
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets and sets the identifier for the administrator of the point earner
        /// </summary>
        public int AdministratorId { get; set; }

        /// <summary>
        /// Gets and sets the point earners user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets and sets the point earners password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets and sets the point earners email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets and sets the first name of the Point Earner
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets and sets the last name of teh Point Earner
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets and sets the calculated Points Earned 
        /// </summary>
        public double PointsEarned { get; set; }

        /// <summary>
        /// Gets and sets all the Charts associated with this Point Earner
        /// </summary>
        public IList<Chart> Charts { get; set; }
 
        /// <summary>
        /// Gets and sets all of the points spent by this earner
        /// </summary>
        public IList<PointsSpent> PointsSpent { get; set; }

        /// <summary>
        /// Calculated the total points spent
        /// </summary>
        public double TotalPointsSpent
        {
            get
            {
                double retVal = 0.0;

                if (this.pointsSpent < 0)
                {
                    this.pointsSpent = 0;

                    if (this.PointsSpent != null)
                    {
                        for (int i = 0; i < this.PointsSpent.Count; i++)
                        {
                            retVal += this.PointsSpent[i].Amount;
                            this.pointsSpent += this.PointsSpent[i].Amount;
                        }
                    }

                    retVal = this.pointsSpent;
                }

                return retVal;
            }
        }

        /// <summary>
        /// Calculate how many points are available.
        /// </summary>
        public double PointsAvailable 
        {
            get { return this.PointsEarned - this.TotalPointsSpent; }
        }    
    }
}
