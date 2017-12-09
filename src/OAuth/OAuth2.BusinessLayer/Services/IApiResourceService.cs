using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    public interface IApiResourceService
    {
        IList<ProtectedApiResource> GetAll();

        ProtectedApiResource GetById(long id);

        ProtectedApiResource GetByName(string name);

        IList<ProtectedApiResource> GetByScopes(IList<string> scopes);

        ProtectedApiResource Add(string name, string displayName, string description, bool enabled);

        ProtectedApiResource Update(long id, string name, string displayName, string description, bool enabled);

        ProtectedApiResource AddSecret(long id, string secret, string encryptionType);

        ProtectedApiResource AddClaim(long id, string claim);

        ProtectedApiResource AddScope(long id, string name, string description);

        bool DeleteScope(long resourceId, long scopeId);
    }
}
