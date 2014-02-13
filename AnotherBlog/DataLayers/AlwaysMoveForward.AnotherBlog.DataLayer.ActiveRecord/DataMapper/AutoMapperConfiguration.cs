using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DomainModel.Poll;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class AutoMapperConfiguration
    {        
        public static void Configure()
        {
            DbInfoMapper.ConfigureAutoMapper();
            SiteInfoDataMap.ConfigureAutoMapper();
            RoleDataMap.ConfigureAutoMapper();
            UserDataMap.ConfigureAutoMapper();
            PollQuestionDataMap.ConfigureAutoMapper();
            TagDataMap.ConfigureAutoMapper();
            BlogPostDataMap.ConfigureAutoMapper();
            BlogDataMap.ConfigureAutoMapper();
            ListDataMap.ConfigureAutoMapper();
            BlogUserDataMap.ConfigureAutoMapper();
            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}
