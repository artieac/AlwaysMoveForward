using AlwaysMoveForward.Core.Common.DataLayer;
using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    public class ClientDataMapper : DataMapBase<Client, Models.Clients>
    {
        /// The static constructor sets up automapper
        /// </summary>
        static ClientDataMapper()
        {
            DataMapConfiguration.Configure();
        }

        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ClientRedirectUri, Models.ClientRedirectUris>()
                .ForMember(source => source.Client, opt => opt.Ignore());
            cfg.CreateMap<Models.ClientRedirectUris, ClientRedirectUri>();
            cfg.CreateMap<ClientSecret, Models.ClientSecrets>()
                .ForMember(source => source.Client, opt => opt.Ignore());
            cfg.CreateMap<Models.ClientSecrets, ClientSecret>();
            cfg.CreateMap<ClientScope, Models.ClientScopes>()
                .ForMember(source => source.Client, opt => opt.Ignore());
            cfg.CreateMap<Models.ClientScopes, ClientScope>();
            cfg.CreateMap<Client, Models.Clients>()
                .ForMember(source => source.ClientRedirectUris, opt => opt.Ignore())
                .ForMember(source => source.ClientSecrets, opt => opt.Ignore())
                .ForMember(source => source.ClientScopes, opt => opt.Ignore())
                .AfterMap((source, destination) =>
                {
                    destination.ClientRedirectUris = ClientRedirectUriListResolver.MapList(source, destination);
                    destination.ClientSecrets = ClientSecretsListResolver.MapList(source, destination);
                    destination.ClientScopes = ClientScopesListResolver.MapList(source, destination);
                });
            cfg.CreateMap<Models.Clients, Client>();
        }
        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Client Map(Models.Clients source, Client destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Models.Clients Map(Client source, Models.Clients destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
