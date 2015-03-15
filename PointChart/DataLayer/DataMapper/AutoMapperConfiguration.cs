using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<CompletedTask, CompletedTaskDTO>()
                .ForMember(ct => ct.AdministratorId, opt => opt.Ignore())
                .ForMember(ct => ct.Chart, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<CompletedTaskDTO, CompletedTask>();
            AutoMapper.Mapper.CreateMap<PointsSpent, PointsSpentDTO>()
                .ForMember(ps => ps.PointEarner, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<PointsSpentDTO, PointsSpent>();
            AutoMapper.Mapper.CreateMap<PointEarner, PointEarnerDTO>();
            AutoMapper.Mapper.CreateMap<PointEarnerDTO, PointEarner>();
            AutoMapper.Mapper.CreateMap<Task, TaskDTO>()
                .ForMember(t => t.Charts, opt => opt.Ignore())
                .ForMember(t => t.CompletedTasks, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<TaskDTO, Task>();
            AutoMapper.Mapper.CreateMap<Chart, ChartDTO>()
                .ForMember(c => c.PointEarner, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<ChartDTO, Chart>();
            AutoMapper.Mapper.AssertConfigurationIsValid();

        }
    }
}
