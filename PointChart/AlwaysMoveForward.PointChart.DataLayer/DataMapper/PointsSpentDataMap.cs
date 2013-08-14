using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class PointsSpentDataMap 
    {
        public PointsSpentDTO Map(PointsSpent source, PointEarnerDTO pointEarner)
        {
            PointsSpentDTO retVal = null;

            if (source != null)
            {
                retVal = new PointsSpentDTO();
                retVal.Id = source.Id;
                retVal.DateSpent = source.DateSpent;
                retVal.Amount = source.Amount;
                retVal.Description = source.Description;
                retVal.PointEarner = pointEarner;
            }

            return retVal;
        }

        public PointsSpent Map(PointsSpentDTO source, PointEarner pointEarner)
        {
            PointsSpent retVal = null;

            if (source != null)
            {
                retVal = new PointsSpent();
                retVal.Id = source.Id;
                retVal.DateSpent = source.DateSpent;
                retVal.Amount = source.Amount;
                retVal.Description = source.Description;
                retVal.PointEarner = pointEarner;
            }

            return retVal;
        }

        public IList<PointsSpent> Map(IList<PointsSpentDTO> source, PointEarner pointEarner)
        {
            IList<PointsSpent> retVal = new List<PointsSpent>();

            if(source!=null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i], pointEarner));
                }
            }

            return retVal;
        }

        public IList<PointsSpentDTO> Map(IList<PointsSpent> source, PointEarnerDTO pointEarner)
        {
            IList<PointsSpentDTO> retVal = new List<PointsSpentDTO>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i], pointEarner));
                }
            }

            return retVal;
        }
    }
}
