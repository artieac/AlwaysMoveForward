using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers
{
    class ClientRedirectUriListResolver : MappedListResolver<Client, Models.Clients, ClientRedirectUri, Models.ClientRedirectUris>
    {
        public static IList<Models.ClientRedirectUris> MapList(Client source, Models.Clients destination)
        {
            ClientRedirectUriListResolver resolver = new ClientRedirectUriListResolver(source, destination);
            return resolver.MapList();
        }

        public ClientRedirectUriListResolver(Client source, Models.Clients destination) : base(source, destination) { }

        protected override Models.ClientRedirectUris FindItemInList(IList<Models.ClientRedirectUris> destinationList, ClientRedirectUri searchTarget)
        {
            return destinationList.Where(sourceItem => sourceItem.Id > 0 && sourceItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override ClientRedirectUri FindItemInList(IList<ClientRedirectUri> sourceList, Models.ClientRedirectUris searchTarget)
        {
            return sourceList.Where(destinationItem => destinationItem.Id > 0 && destinationItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override IList<Models.ClientRedirectUris> GetDestinationList()
        {
            return new List<Models.ClientRedirectUris>(this.DestinationContainer.ClientRedirectUris);
        }

        protected override IList<ClientRedirectUri> GetSourceList()
        {
            return this.SourceContainer.ClientRedirectUris;
        }

        protected override void SetDestinationContainer(Models.ClientRedirectUris destinationChildListItem, Models.Clients destinationContainer)
        {
            destinationChildListItem.Client = destinationContainer;
        }
    }
}

