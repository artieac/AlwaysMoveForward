using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Text;
using AlwaysMoveForward.OAuth2.DataLayer.Models;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper.ListResolvers
{
    internal class ApiSecretListResolver : MappedListResolver<ProtectedApiResource, Models.ApiResources, ProtectedApiSecret, Models.ApiSecrets>
    {
        public static IList<Models.ApiSecrets> MapList(ProtectedApiResource source, Models.ApiResources destination)
        {
            ApiSecretListResolver resolver = new ApiSecretListResolver(source, destination);
            return resolver.MapList();
        }

        public ApiSecretListResolver(ProtectedApiResource source, Models.ApiResources destination) : base(source, destination) { }

        protected override ApiSecrets FindItemInList(IList<ApiSecrets> destinationList, ProtectedApiSecret searchTarget)
        {
            return destinationList.Where(sourceItem => sourceItem.Id > 0 && sourceItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override ProtectedApiSecret FindItemInList(IList<ProtectedApiSecret> sourceList, ApiSecrets searchTarget)
        {
            return sourceList.Where(destinationItem => destinationItem.Id > 0 && destinationItem.Id == searchTarget.Id).FirstOrDefault();
        }

        protected override IList<ApiSecrets> GetDestinationList()
        {
            return new List<ApiSecrets>(this.DestinationContainer.ApiSecrets);
        }

        protected override IList<ProtectedApiSecret> GetSourceList()
        {
            return this.SourceContainer.ApiSecrets;
        }

        protected override void SetDestinationContainer(Models.ApiSecrets destinationChildListItem, Models.ApiResources destinationContainer)
        {
            destinationChildListItem.ApiResource = destinationContainer;
        }
    }
}
