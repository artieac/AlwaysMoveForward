using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class CompletedTaskDataMap : DataMapBase<CompletedTask, CompletedTaskDTO>
    {
        public override CompletedTaskDTO Map(CompletedTask source, CompletedTaskDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override CompletedTask Map(CompletedTaskDTO source, CompletedTask destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
