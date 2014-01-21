using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    public class PointEarner
    {
        private double pointsSpent;

        public PointEarner()
        {
            pointsSpent = -1;
            this.Id = -1;
        }

        public int Id { get; set; }
        public int AdministratorId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public double PointsEarned { get; set; }
        public IList<Chart> Charts { get; set; }
 
        public IList<PointsSpent> PointsSpent { get; set; }

        public double TotalPointsSpent
        {
            get
            {
                double retVal = 0.0;

                if (pointsSpent < 0)
                {
                    pointsSpent = 0;

                    if (this.PointsSpent != null)
                    {
                        for (int i = 0; i < this.PointsSpent.Count; i++)
                        {
                            retVal += this.PointsSpent[i].Amount;
                            pointsSpent += this.PointsSpent[i].Amount;
                        }
                    }

                    retVal = pointsSpent;
                }

                return retVal;
            }
        }

        public double PointsAvailable 
        {
            get { return this.PointsEarned - this.TotalPointsSpent; }
        }    
    }
}
