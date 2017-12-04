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
                    cfg.CreateMap<ApiClaims, Models.ApiClaims>();
                    cfg.CreateMap<Models.ApiClaims, ApiClaims>();
                    cfg.CreateMap<ApiScopeClaims, Models.ApiScopeClaims>();
                    cfg.CreateMap<Models.ApiScopeClaims, ApiScopeClaims>();
                    cfg.CreateMap<ApiSecrets, Models.ApiSecrets>();
                    cfg.CreateMap<Models.ApiSecrets, ApiSecrets>();
                    cfg.CreateMap<ApiScopes, Models.ApiScopes>();
                    cfg.CreateMap<Models.ApiScopes, ApiScopes>();
                    cfg.CreateMap<ApiResources, Models.ApiResources>();
                    cfg.CreateMap<Models.ApiResources, ApiResources>();
                });

#if DEBUG
                Mapper.AssertConfigurationIsValid();
#endif

                DataMapConfiguration.isConfigured = true;
            }
        }
    }
}
