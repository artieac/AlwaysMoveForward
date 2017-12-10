using AlwaysMoveForward.Core.Common.DataLayer;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
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
                    if (source.ApiSecrets != null)
                    {
                        foreach (var child in source.ApiSecrets)
                        {
                            Models.ApiSecrets dtoChild = destination.ApiSecrets.Where(destResource => destResource.Id == child.Id).FirstOrDefault();

                            if (dtoChild != null)
                            {
                                dtoChild.ApiResourceId = child.ApiResourceId;
                                dtoChild.Expiration = child.Expiration;
                                dtoChild.Type = child.Type;
                                dtoChild.Value = child.Value;
                            }
                            else
                            {
                                destination.ApiSecrets.Add(Mapper.Map(child, new Models.ApiSecrets()));
                            }
                        }
                    }

                    foreach (var child in destination.ApiSecrets)
                        child.ApiResource = destination;

                    if (source.ApiClaims != null)
                    {
                        foreach (var child in source.ApiClaims)
                        {
                            Models.ApiClaims dtoChild = destination.ApiClaims.Where(destResource => destResource.Id == child.Id).FirstOrDefault();

                            if (dtoChild != null)
                            {
                                dtoChild.ApiResourceId = child.ApiResourceId;
                                dtoChild.Type = child.Type;
                            }
                            else
                            {
                                destination.ApiClaims.Add(Mapper.Map(child, new Models.ApiClaims()));
                            }
                        }
                    }

                    foreach (var child in destination.ApiClaims)
                        child.ApiResource = destination;

                    if (source.ApiScopes != null)
                    {
                        foreach (var child in source.ApiScopes)
                        {
                            Models.ApiScopes dtoChild = destination.ApiScopes.Where(destResource => destResource.Id == child.Id).FirstOrDefault();

                            if (dtoChild != null)
                            {
                                dtoChild.ApiResourceId = child.ApiResourceId;
                                //                            dtoChild.ApiScopeClaims = child.ApiScopeClaims;
                                dtoChild.Description = child.Description;
                                dtoChild.DisplayName = child.DisplayName;
                                dtoChild.Emphasize = child.Emphasize;
                                dtoChild.Name = child.Name;
                                dtoChild.Required = child.Required;
                                dtoChild.ShowInDiscoveryDocument = child.ShowInDiscoveryDocument;
                            }
                            else
                            {
                                destination.ApiScopes.Add(Mapper.Map(child, new Models.ApiScopes()));
                            }
                        }
                    }

                    foreach (var child in destination.ApiScopes)
                        child.ApiResource = destination;

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
