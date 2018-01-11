using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
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
                    cfg.CreateMap<LoginAttempt, Models.LoginAttempts>();
                    cfg.CreateMap<Models.LoginAttempts, LoginAttempt>();
                    cfg.CreateMap<AMFUserLogin, Models.Amfusers>();
                    cfg.CreateMap<Models.Amfusers, AMFUserLogin>();
                    cfg.CreateMap<Consumer, Models.Consumers>();
                    cfg.CreateMap<Models.Consumers, Consumer>();
                    ApiResourceDataMapper.Configure(cfg);
                    ClientDataMapper.Configure(cfg);
                });

#if DEBUG
                Mapper.AssertConfigurationIsValid();
#endif

                DataMapConfiguration.isConfigured = true;
            }
        }
    }
}
