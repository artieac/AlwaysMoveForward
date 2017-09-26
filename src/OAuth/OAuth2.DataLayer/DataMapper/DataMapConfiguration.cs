using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    public class DataMapConfiguration
    {
        private static bool isConfigured = false;

        public static void Configure()
        {
            if(isConfigured == false)
            {
                Mapper.Initialize(cfg => {
                    cfg.CreateMap<LoginAttempt, DTO.LoginAttempt>();
                    cfg.CreateMap<DTO.LoginAttempt, LoginAttempt>();
                    cfg.CreateMap<AMFUserLogin, DTO.AMFUser>();
                    cfg.CreateMap<DTO.AMFUser, AMFUserLogin>();
                    cfg.CreateMap<Consumer, DTO.Consumer>();
                    cfg.CreateMap<DTO.Consumer, Consumer>();
                });

#if DEBUG
                Mapper.AssertConfigurationIsValid();
#endif

                DataMapConfiguration.isConfigured = true;
            }
        }
    }
}
