using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class CompletedTaskDataMap : DataMapBase<CompletedTask, DTO.CompletedTask>
    {
        static CompletedTaskDataMap()
        {
            CompletedTaskDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<CompletedTask, DTO.CompletedTask>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<CompletedTask, DTO.CompletedTask>();
                newMap.MaxDepth(2);
            }

            existingMap = Mapper.FindTypeMapFor<DTO.CompletedTask, CompletedTask>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<DTO.CompletedTask, CompletedTask>();
                newMap.MaxDepth(2);
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif        
        }

        public override DTO.CompletedTask Map(CompletedTask source, DTO.CompletedTask destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override CompletedTask Map(DTO.CompletedTask source, CompletedTask destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
