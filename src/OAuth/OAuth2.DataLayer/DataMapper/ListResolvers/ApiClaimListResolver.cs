using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers
{
    internal class ApiClaimListResolver : MappedListResolver<ProtectedApiResource, Models.ApiResources, ProtectedApiClaim, Models.ApiClaims>
    {
        public static IList<Models.ApiClaims> MapList(ProtectedApiResource source, Models.ApiResources destination)
        {
            ApiClaimListResolver resolver = new ApiClaimListResolver(source, destination);
            return resolver.MapList();
        }

        public ApiClaimListResolver(ProtectedApiResource source, Models.ApiResources destination) : base(source, destination) { }

        protected override Models.ApiClaims FindItemInList(IList<Models.ApiClaims> destinationList, ProtectedApiClaim searchTarget)
        {
            return destinationList.Where(sourceItem => sourceItem.Id > 0 && sourceItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override ProtectedApiClaim FindItemInList(IList<ProtectedApiClaim> sourceList, Models.ApiClaims searchTarget)
        {
            return sourceList.Where(destinationItem => destinationItem.Id > 0 && destinationItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override IList<Models.ApiClaims> GetDestinationList()
        {
            return new List<Models.ApiClaims>(this.DestinationContainer.ApiClaims);
        }

        protected override IList<ProtectedApiClaim> GetSourceList()
        {
            return this.SourceContainer.ApiClaims;
        }

        protected override void SetDestinationContainer(Models.ApiClaims destinationChildListItem, Models.ApiResources destinationContainer)
        {
            destinationChildListItem.ApiResource = destinationContainer;
        }
    }
}
