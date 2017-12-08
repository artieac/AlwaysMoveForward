using AlwaysMoveForward.Core.Common.DataLayer;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    internal class ApiResourceDataMapper : DataMapBase<ApiResources, Models.ApiResources>
    {       
        /// The static constructor sets up automapper
        /// </summary>
        static ApiResourceDataMapper()
        {
            DataMapConfiguration.Configure();
        }

        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ApiClaims, Models.ApiClaims>()
                .ForMember(source => source.ApiResource, opt => opt.Ignore());
            cfg.CreateMap<Models.ApiClaims, ApiClaims>();
            cfg.CreateMap<ApiScopeClaims, Models.ApiScopeClaims>();
            cfg.CreateMap<Models.ApiScopeClaims, ApiScopeClaims>();
            cfg.CreateMap<ApiSecrets, Models.ApiSecrets>()
                .ForMember(source => source.ApiResource, opt => opt.Ignore());
            cfg.CreateMap<Models.ApiSecrets, ApiSecrets>();
            cfg.CreateMap<ApiScopes, Models.ApiScopes>();
            cfg.CreateMap<Models.ApiScopes, ApiScopes>();
            cfg.CreateMap<ApiResources, Models.ApiResources>()
                .AfterMap((source, destination) =>
                {
                    foreach (var child in destination.ApiSecrets)
                        child.ApiResource = destination;
                    foreach (var child in destination.ApiClaims)
                        child.ApiResource = destination;
                });
            cfg.CreateMap<Models.ApiResources, ApiResources>();
        }
        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override ApiResources Map(Models.ApiResources source, ApiResources destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Models.ApiResources Map(ApiResources source, Models.ApiResources destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
