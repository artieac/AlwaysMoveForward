using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class PointEarnerDataMap
    {
        #region PointEarner aggregate Root

        public PointEarnerDTO Map(PointEarner source, ChartDTO ownerChart)
        {
            PointEarnerDTO retVal = null;

            if (source != null)
            {
                retVal = new PointEarnerDTO();
                retVal.Id = source.Id;
                retVal.UserName = source.UserName;
                retVal.Password = source.Password;
                retVal.Email = source.Email;
                retVal.FirstName = source.FirstName;
                retVal.LastName = source.LastName;

                retVal.Charts = new List<ChartDTO>();
                retVal.Charts.Add(ownerChart);

                retVal.PointsSpent = DataMapManager.Mappers().PointsSpent.Map(source.PointsSpent, retVal);
            }

            return retVal;
        }

        public PointEarner Map(PointEarnerDTO source, Chart ownerChart)
        {
            PointEarner retVal = null;

            if (source != null)
            {
                retVal = new PointEarner();
                retVal.Id = source.Id;
                retVal.Id = source.Id;
                retVal.UserName = source.UserName;
                retVal.Password = source.Password;
                retVal.Email = source.Email;
                retVal.FirstName = source.FirstName;
                retVal.LastName = source.LastName;

                retVal.Charts = new List<Chart>();
                retVal.Charts.Add(ownerChart);

                retVal.PointsSpent = DataMapManager.Mappers().PointsSpent.Map(source.PointsSpent, retVal);
            }

            return retVal;
        }

        #endregion

        #region Chart Aggregate Root

        public PointEarnerDTO Map(PointEarner source)
        {
            PointEarnerDTO retVal = null;

            if (source != null)
            {
                retVal = new PointEarnerDTO();
                retVal.Id = source.Id;
                retVal.UserName = source.UserName;
                retVal.Password = source.Password;
                retVal.Email = source.Email;
                retVal.FirstName = source.FirstName;
                retVal.LastName = source.LastName;

                retVal.Charts = DataMapManager.Mappers().Chart.Map(source.Charts, retVal);
                retVal.PointsSpent = DataMapManager.Mappers().PointsSpent.Map(source.PointsSpent, retVal);
            }

            return retVal;
        }

        public PointEarner Map(PointEarnerDTO source)
        {
            PointEarner retVal = null;

            if (source != null)
            {
                retVal = new PointEarner();
                retVal.Id = source.Id;
                retVal.Id = source.Id;
                retVal.UserName = source.UserName;
                retVal.Password = source.Password;
                retVal.Email = source.Email;
                retVal.FirstName = source.FirstName;
                retVal.LastName = source.LastName;

                retVal.Charts = DataMapManager.Mappers().Chart.Map(source.Charts, retVal);
                retVal.PointsSpent = DataMapManager.Mappers().PointsSpent.Map(source.PointsSpent, retVal);
            }

            return retVal;
        }

        #endregion
    }
}
