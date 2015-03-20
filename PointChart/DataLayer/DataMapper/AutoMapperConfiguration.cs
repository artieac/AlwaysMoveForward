using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<CompletedTask, DTO.CompletedTask>()
                .ForMember(ct => ct.Chart, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<DTO.CompletedTask, CompletedTask>();
            AutoMapper.Mapper.CreateMap<DTO.PointsSpent, PointsSpent>();
            AutoMapper.Mapper.CreateMap<PointEarner, DTO.PointEarner>();
            AutoMapper.Mapper.CreateMap<DTO.PointEarner, PointEarner>();
            AutoMapper.Mapper.CreateMap<Task, DTO.Task>()
                .ForMember(t => t.Charts, opt => opt.Ignore())
                .ForMember(t => t.CompletedTasks, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<DTO.Task, Task>();
            AutoMapper.Mapper.CreateMap<DTO.Chart, Chart>();
            AutoMapper.Mapper.AssertConfigurationIsValid();

        }
    }
}
