using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class ChartDataMap : DataMapBase<Chart, DTO.Chart>
    {
        static ChartDataMap()
        {
            ChartDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            PointChartUserDataMap.ConfigureAutoMapper();
            TaskDataMap.ConfigureAutoMapper();

            var existingMap = Mapper.FindTypeMapFor<Chart, DTO.Chart>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<Chart, DTO.Chart>()
                    .ForMember(dest => dest.Tasks, src => src.ResolveUsing<TaskDTOListResolver>());
                newMap.MaxDepth(2);
            }

            existingMap = Mapper.FindTypeMapFor<DTO.Chart, Chart>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<DTO.Chart, Chart>();
                newMap.MaxDepth(2);
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif        
        }

        #region Chart Aggregate Root

        public override DTO.Chart Map(Chart source, DTO.Chart destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override Chart Map(DTO.Chart source, Chart destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        #endregion
    }
}
