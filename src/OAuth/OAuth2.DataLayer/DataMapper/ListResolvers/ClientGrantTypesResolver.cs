using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers
{
    public class ClientGrantTypesResolver : MappedListResolver<Client, Models.Clients, ClientGrantType, Models.ClientGrantTypes>
    {
        public static IList<Models.ClientGrantTypes> MapList(Client source, Models.Clients destination)
        {
            ClientGrantTypesResolver resolver = new ClientGrantTypesResolver(source, destination);
            return resolver.MapList();
        }

        public ClientGrantTypesResolver(Client source, Models.Clients destination) : base(source, destination) { }

        protected override Models.ClientGrantTypes FindItemInList(IList<Models.ClientGrantTypes> destinationList, ClientGrantType searchTarget)
        {
            return destinationList.Where(sourceItem => sourceItem.Id > 0 && sourceItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override ClientGrantType FindItemInList(IList<ClientGrantType> sourceList, Models.ClientGrantTypes searchTarget)
        {
            return sourceList.Where(destinationItem => destinationItem.Id > 0 && destinationItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override IList<Models.ClientGrantTypes> GetDestinationList()
        {
            return new List<Models.ClientGrantTypes>(this.DestinationContainer.ClientGrantTypes);
        }

        protected override IList<ClientGrantType> GetSourceList()
        {
            return this.SourceContainer.ClientGrantTypes;
        }

        protected override void SetDestinationContainer(Models.ClientGrantTypes destinationChildListItem, Models.Clients destinationContainer)
        {
            destinationChildListItem.Client = destinationContainer;
        }
    }
}




