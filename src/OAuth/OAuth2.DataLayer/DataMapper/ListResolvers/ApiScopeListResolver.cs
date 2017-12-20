using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers
{
    internal class ApiScopeListResolver : MappedListResolver<ProtectedApiResource, Models.ApiResources, ProtectedApiScope, Models.ApiScopes>
    {
        public static IList<Models.ApiScopes> MapList(ProtectedApiResource source, Models.ApiResources destination)
        {
            ApiScopeListResolver resolver = new ApiScopeListResolver(source, destination);
            return resolver.MapList();
        }

        public ApiScopeListResolver(ProtectedApiResource source, Models.ApiResources destination) : base(source, destination) { }

        protected override Models.ApiScopes FindItemInList(IList<Models.ApiScopes> destinationList, ProtectedApiScope searchTarget)
        {
            return destinationList.Where(sourceItem => sourceItem.Id > 0 && sourceItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override ProtectedApiScope FindItemInList(IList<ProtectedApiScope> sourceList, Models.ApiScopes searchTarget)
        {
            return sourceList.Where(destinationItem => destinationItem.Id > 0 && destinationItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override IList<Models.ApiScopes> GetDestinationList()
        {
            return new List<Models.ApiScopes>(this.DestinationContainer.ApiScopes);
        }

        protected override IList<ProtectedApiScope> GetSourceList()
        {
            return this.SourceContainer.ApiScopes;
        }

        protected override void SetDestinationContainer(Models.ApiScopes destinationChildListItem, Models.ApiResources destinationContainer)
        {
            destinationChildListItem.ApiResource = destinationContainer;
        }
    }

}
