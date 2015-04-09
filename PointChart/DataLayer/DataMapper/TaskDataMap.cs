using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class TaskDataMap : DataMapBase<Task, DTO.Task>
    {
        static TaskDataMap()
        {
            TaskDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            CompletedTaskDataMap.ConfigureAutoMapper();

            var existingMap = Mapper.FindTypeMapFor<Task, DTO.Task>();
            if (existingMap == null)
            {
                Mapper.CreateMap<Task, DTO.Task>()
                    .MaxDepth(2);
            }

            existingMap = Mapper.FindTypeMapFor<DTO.Task, Task>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<DTO.Task, Task>();
                newMap.MaxDepth(2);
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif        
        }

        public override DTO.Task Map(Task source, DTO.Task destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override Task Map(DTO.Task source, Task destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
