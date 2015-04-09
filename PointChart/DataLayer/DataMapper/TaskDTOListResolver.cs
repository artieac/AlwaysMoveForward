using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class TaskDTOListResolver : MappedListResolver<Task, DTO.Task>
    {
        protected override IList<DTO.Task> GetDestinationList(ResolutionResult source)
        {
            return ((DTO.Chart)source.Context.DestinationValue).Tasks;
        }

        protected override IList<Task> GetSourceList(ResolutionResult source)
        {
            IList<Task> retVal = null;

            if (source.Value != null)
            {
                retVal = ((Chart)source.Value).Tasks;
            }

            return retVal;
        }

        protected override DTO.Task FindItemInList(IList<DTO.Task> destinationList, Task searchTarget)
        {
            return destinationList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }

        protected override Task FindItemInList(IList<Task> sourceList, DTO.Task searchTarget)
        {
            return sourceList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }
    }
}
