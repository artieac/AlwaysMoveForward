using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers
{
    class ClientScopesListResolver : MappedListResolver<Client, Models.Clients, ClientScope, Models.ClientScopes>
    {
        public static IList<Models.ClientScopes> MapList(Client source, Models.Clients destination)
        {
            ClientScopesListResolver resolver = new ClientScopesListResolver(source, destination);
            return resolver.MapList();
        }

        public ClientScopesListResolver(Client source, Models.Clients destination) : base(source, destination) { }

        protected override Models.ClientScopes FindItemInList(IList<Models.ClientScopes> destinationList, ClientScope searchTarget)
        {
            return destinationList.Where(sourceItem => sourceItem.Id > 0 && sourceItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override ClientScope FindItemInList(IList<ClientScope> sourceList, Models.ClientScopes searchTarget)
        {
            return sourceList.Where(destinationItem => destinationItem.Id > 0 && destinationItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override IList<Models.ClientScopes> GetDestinationList()
        {
            return new List<Models.ClientScopes>(this.DestinationContainer.ClientScopes);
        }

        protected override IList<ClientScope> GetSourceList()
        {
            return this.SourceContainer.ClientScopes;
        }

        protected override void SetDestinationContainer(Models.ClientScopes destinationChildListItem, Models.Clients destinationContainer)
        {
            destinationChildListItem.Client = destinationContainer;
        }
    }
}



