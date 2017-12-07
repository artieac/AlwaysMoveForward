using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    public interface IApiResourcesService
    {
        IList<ApiResources> GetAll();

        ApiResources GetById(long id);

        ApiResources Add(string name, string displayName, string description, bool enabled);

        ApiResources Update(long id, string name, string displayName, string description, bool enabled);

        ApiResources AddSecret(long id, string secret);

        ApiResources AddClaim(long id, string claim);
    }
}
