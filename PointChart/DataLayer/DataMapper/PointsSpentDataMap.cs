using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class PointsSpentDataMap : DataMapBase<PointsSpent, PointsSpentDTO>
    {
        public override PointsSpentDTO Map(PointsSpent source, PointsSpentDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PointsSpent Map(PointsSpentDTO source, PointsSpent destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
