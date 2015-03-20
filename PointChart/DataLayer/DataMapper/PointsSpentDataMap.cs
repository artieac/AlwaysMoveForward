using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class PointsSpentDataMap : DataMapBase<PointsSpent, DTO.PointsSpent>
    {
        static PointsSpentDataMap()
        {
            PointsSpentDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<PointsSpent, DTO.PointsSpent>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<PointsSpent, DTO.PointsSpent>();
                newMap.MaxDepth(2);
            }

            existingMap = Mapper.FindTypeMapFor<DTO.PointsSpent, PointsSpent>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<DTO.PointsSpent, PointsSpent>();
                newMap.MaxDepth(2);
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif        
        }

        public override DTO.PointsSpent Map(PointsSpent source, DTO.PointsSpent destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PointsSpent Map(DTO.PointsSpent source, PointsSpent destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
