using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class UserDTOListResolver : MappedListResolver<PointChartUser, DTO.User>
    {
        protected override IList<DTO.User> GetDestinationList(ResolutionResult source)
        {
            return ((DTO.User)source.Context.DestinationValue).PointEarners;
        }

        protected override IList<PointChartUser> GetSourceList(ResolutionResult source)
        {
            IList<PointChartUser> retVal = null;

            if (source.Value != null)
            {
                retVal = ((PointChartUser)source.Value).PointEarners;
            }

            return retVal;
        }

        protected override DTO.User FindItemInList(IList<DTO.User> destinationList, PointChartUser searchTarget)
        {
            return destinationList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }

        protected override PointChartUser FindItemInList(IList<PointChartUser> sourceList, DTO.User searchTarget)
        {
            return sourceList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }

    }
}
