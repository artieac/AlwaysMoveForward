using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers
{
    class ClientSecretsListResolver : MappedListResolver<Client, Models.Clients, ClientSecret, Models.ClientSecrets>
    {
        public static IList<Models.ClientSecrets> MapList(Client source, Models.Clients destination)
        {
            ClientSecretsListResolver resolver = new ClientSecretsListResolver(source, destination);
            return resolver.MapList();
        }

        public ClientSecretsListResolver(Client source, Models.Clients destination) : base(source, destination) { }

        protected override Models.ClientSecrets FindItemInList(IList<Models.ClientSecrets> destinationList, ClientSecret searchTarget)
        {
            return destinationList.Where(sourceItem => sourceItem.Id > 0 && sourceItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override ClientSecret FindItemInList(IList<ClientSecret> sourceList, Models.ClientSecrets searchTarget)
        {
            return sourceList.Where(destinationItem => destinationItem.Id > 0 && destinationItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override IList<Models.ClientSecrets> GetDestinationList()
        {
            return new List<Models.ClientSecrets>(this.DestinationContainer.ClientSecrets);
        }

        protected override IList<ClientSecret> GetSourceList()
        {
            return this.SourceContainer.ClientSecrets;
        }

        protected override void SetDestinationContainer(Models.ClientSecrets destinationChildListItem, Models.Clients destinationContainer)
        {
            destinationChildListItem.Client = destinationContainer;
        }
    }
}

