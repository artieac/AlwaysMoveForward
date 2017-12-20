using AlwaysMoveForward.Core.Common.DataLayer;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    internal class ApiResourceDataMapper : DataMapBase<ProtectedApiResource, Models.ApiResources>
    {       
        /// The static constructor sets up automapper
        /// </summary>
        static ApiResourceDataMapper()
        {
            DataMapConfiguration.Configure();
        }

        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProtectedApiClaim, Models.ApiClaims>()
                .ForMember(source => source.ApiResource, opt => opt.Ignore());
            cfg.CreateMap<Models.ApiClaims, ProtectedApiClaim>();
            cfg.CreateMap<ProtectedApiScopeClaim, Models.ApiScopeClaims>();
            cfg.CreateMap<Models.ApiScopeClaims, ProtectedApiScopeClaim>();
            cfg.CreateMap<ProtectedApiSecret, Models.ApiSecrets>()
                .ForMember(source => source.ApiResource, opt => opt.Ignore());
            cfg.CreateMap<Models.ApiSecrets, ProtectedApiSecret>();
            cfg.CreateMap<ProtectedApiScope, Models.ApiScopes>()
                .ForMember(source => source.ApiResource, opt => opt.Ignore());
            cfg.CreateMap<Models.ApiScopes, ProtectedApiScope>();
            cfg.CreateMap<ProtectedApiResource, Models.ApiResources>()
                .ForMember(source => source.ApiSecrets, opt => opt.Ignore())
                .ForMember(source => source.ApiClaims, opt => opt.Ignore())
                .ForMember(source => source.ApiScopes, opt => opt.Ignore())
                .AfterMap((source, destination) =>
                {
                    destination.ApiSecrets = ApiSecretListResolver.MapList(source, destination);
                    destination.ApiClaims = ApiClaimListResolver.MapList(source, destination);
                    destination.ApiScopes = ApiScopeListResolver.MapList(source, destination);
                });
            cfg.CreateMap<Models.ApiResources, ProtectedApiResource>();
        }
        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override ProtectedApiResource Map(Models.ApiResources source, ProtectedApiResource destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Models.ApiResources Map(ProtectedApiResource source, Models.ApiResources destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
