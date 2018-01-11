using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    public interface IClientService
    {
        IList<Client> GetAll();

        Client GetById(long id);

        Client Add(string name, string displayName, string description, bool enabled);

        Client Update(long id, string name, string displayName, string description, bool enabled);

        Client AddSecret(long id, string secret, string encryptionType, string description);

        bool DeleteSecret(long clientId, int secretId);

        Client UpdateScopes(long id, IList<String> scopes);

        Client AddRedirectUri(long clientId, string redirectUri);

        bool DeleteRedirectUri(long clientId, int redirectUriId);

        Client AddGrantType(long clientId, string grantType);

        bool DeleteGrantType(long clientId, string grantType);
    }
}
