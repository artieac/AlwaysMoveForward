using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class PointEarnerService : PointChartService
    {
        public PointEarnerService(IUnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager) : base(unitOfWork, repositoryManager) { }

        public IList<PointEarner> GetAll(PointChartUser currentUser)
        {
            return this.PointChartRepositories.PointEarner.GetAllByAdministratorId(currentUser.Id);
        }

        public PointEarner GetById(long pointEarnerId)
        {
            return this.PointChartRepositories.PointEarner.GetById(pointEarnerId);
        }

        public PointEarner AddOrUpdate(string firstName, string lastName, string email, PointChartUser currentUser)
        {
            PointEarner retVal = null;
            
            if (email == null)
            {
                retVal = this.PointChartRepositories.PointEarner.GetByFirstNameLastName(firstName, lastName, currentUser.Id);
            }
            else
            {
                retVal = this.PointChartRepositories.PointEarner.GetByEmail(email, currentUser.Id);
            }

            if (retVal == null)
            {
                retVal = new PointEarner();
            }

            if (retVal.FirstName != firstName ||
               retVal.LastName != lastName ||
               retVal.Email != email)
            {
                retVal.FirstName = firstName;
                retVal.LastName = lastName;
                retVal.Email = email;

                retVal = this.PointChartRepositories.PointEarner.Save(retVal);
            }

            return retVal;
        }

        public PointEarner SpendPoints(int pointEarnerId, double pointsToSpend, DateTime dateSpent, string description)
        {
            PointEarner retVal = this.GetById(pointEarnerId);

            if (retVal != null)
            {
                PointsSpent pointsSpent = new PointsSpent();
                pointsSpent.Amount = pointsToSpend;
                pointsSpent.DateSpent = dateSpent;
                pointsSpent.Description = description;

                if (retVal.PointsSpent == null)
                {
                    retVal.PointsSpent = new List<PointsSpent>();
                }

                retVal.PointsSpent.Add(pointsSpent);
                retVal = this.PointChartRepositories.PointEarner.Save(retVal);
            }
            
            return retVal;
        }

        public PointEarner DeleteSpentPoints(long pointEarnerId, int spentPointsId)
        {
            PointEarner retVal = this.GetById(pointEarnerId);

            if (retVal != null)
            {
                if (retVal.PointsSpent != null)
                {
                    for (int i = 0; i < retVal.PointsSpent.Count; i++)
                    {
                        if (retVal.PointsSpent[i].Id == spentPointsId)
                        {
                            retVal.PointsSpent.RemoveAt(i);
                            break;
                        }
                    }
                }

                retVal = this.PointChartRepositories.PointEarner.Save(retVal);
            }

            return retVal;
        }
    }
}
