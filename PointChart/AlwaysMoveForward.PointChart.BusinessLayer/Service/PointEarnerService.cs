using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class PointEarnerService : PointChartService
    {
        public interface IDependencies
        {
            IUnitOfWork UnitOfWork { get; }
        }

        public PointEarnerService(IDependencies dependencies, IPointChartRepositoryManager repositoryManager) : base(dependencies.UnitOfWork, repositoryManager) { }

        public IList<PointEarner> GetAll(User currentUser)
        {
            return this.PointChartRepositories.PointEarner.GetAllByAdministratorId(currentUser.UserId);
        }

        public PointEarner GetById(int pointEarnerId)
        {
            return this.PointChartRepositories.PointEarner.GetById(pointEarnerId);
        }

        public PointEarner AddOrUpdate(String firstName, String lastName, String email, User currentUser)
        {
            PointEarner retVal = null;
            
            if(email==null)
            {
                retVal = this.PointChartRepositories.PointEarner.GetByFirstNameLastName(firstName, lastName, currentUser.UserId);
            }
            else
            {
                retVal = this.PointChartRepositories.PointEarner.GetByEmail(email, currentUser.UserId);
            }

            if(retVal==null)
            {
                retVal = new PointEarner();
            }

            retVal.FirstName = firstName;
            retVal.LastName = lastName;

            retVal = this.PointChartRepositories.PointEarner.Save(retVal);
            return retVal;
        }

        public PointEarner SpendPoints(int pointEarnerId, double pointsToSpend, DateTime dateSpent, String description)
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

        public PointEarner DeleteSpentPoints(int pointEarnerId, int spentPointsId)
        {
            PointEarner retVal = this.GetById(pointEarnerId);

            if (retVal != null)
            {
                if(retVal.PointsSpent!=null)
                {
                    for(int i = 0; i < retVal.PointsSpent.Count; i++)
                    {
                        if(retVal.PointsSpent[i].Id==spentPointsId)
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
