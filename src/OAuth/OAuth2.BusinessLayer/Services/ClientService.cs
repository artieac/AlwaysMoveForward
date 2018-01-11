using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using AlwaysMoveForward.OAuth2.DataLayer.Repositories;
using id4 = IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static IdentityServer4.IdentityServerConstants;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    public class ClientService : IClientService
    {
        public ClientService(IClientRepository clientRepository)
        {
            this.ClientRepository = clientRepository;
        }

        public IClientRepository ClientRepository { get; private set; }

        public IList<Client> GetAll()
        {
            return this.ClientRepository.GetAll();
        }

        public Client GetById(long id)
        {
            return this.ClientRepository.GetById(id);
        }

        public Client Add(string name, string displayName, string description, bool enabled)
        {
            Client newItem = new Client();
            newItem.AllowOfflineAccess = true;
            newItem.ClientId = name;
            newItem.ClientName = displayName;
            newItem.ClientUri = "";
            newItem.Description = description;
            newItem.Enabled = enabled;
            newItem.AllowAccessTokensViaBrowser = true;
            newItem.AlwaysIncludeUserClaimsInIdToken = true;
            newItem.AllowOfflineAccess = true;
            newItem.AccessTokenType = (int)id4.AccessTokenType.Jwt;
            newItem.ProtocolType = ProtocolTypes.OpenIdConnect;
            return this.ClientRepository.Save(newItem);
        }

        public Client Update(long id, string name, string displayName, string description, bool enabled)
        {
            Client targetItem = this.ClientRepository.GetById(id);

            if (targetItem != null)
            {
                targetItem.ClientId = name;
                targetItem.ClientName = displayName;
                targetItem.ClientUri = "";
                targetItem.Description = description;
                targetItem.Enabled = enabled;

                targetItem = this.ClientRepository.Save(targetItem);
            }

            return targetItem;
        }

        public Client AddSecret(long id, string secret, string encryptionType, string description)
        {
            Client targetItem = this.ClientRepository.GetById(id);

            if (targetItem != null)
            {
                targetItem.AddSecret(secret, encryptionType, description);
                targetItem = this.ClientRepository.Save(targetItem);
            }

            return targetItem;
        }

        public bool DeleteSecret(long clientId, int secretId)
        {
            bool retVal = false;

            Client targetItem = this.ClientRepository.GetById(clientId);

            if (targetItem != null)
            {
                targetItem.RemoveSecret(secretId);
                this.ClientRepository.Save(targetItem);
                retVal = true;
            }

            return retVal;
        }

        public Client UpdateScopes(long id, IList<string> scopes)
        {
            Client targetItem = this.ClientRepository.GetById(id);

            if (targetItem != null)
            {
                targetItem.UpdateScopes(scopes);
                targetItem = this.ClientRepository.Save(targetItem);
            }

            return targetItem;
        }

        public bool DeleteScope(long clientId, int scopeId)
        {
            bool retVal = false;

            Client targetItem = this.ClientRepository.GetById(clientId);

            if (targetItem != null)
            {
                targetItem.RemoveScope(scopeId);
                this.ClientRepository.Save(targetItem);
                retVal = true;
            }

            return retVal;
        }

        public Client AddRedirectUri(long clientId, string redirectUri)
        {
            Client targetItem = this.ClientRepository.GetById(clientId);

            if (targetItem != null)
            {
                targetItem.AddRedirectUri(redirectUri);
                targetItem = this.ClientRepository.Save(targetItem);
            }

            return targetItem;
        }

        public bool DeleteRedirectUri(long clientId, int redirectUriId)
        {
            bool retVal = false;

            Client targetItem = this.ClientRepository.GetById(clientId);

            if (targetItem != null)
            {
                targetItem.RemoveRedirectUri(redirectUriId);
                this.ClientRepository.Save(targetItem);
                retVal = true;
            }

            return retVal;
        }

        public Client AddGrantType(long clientId, string grantType)
        {
            Client targetItem = this.ClientRepository.GetById(clientId);

            if (targetItem != null)
            {
                targetItem.AddGrantType(grantType);
                targetItem = this.ClientRepository.Save(targetItem);
            }

            return targetItem;

        }

        public bool DeleteGrantType(long clientId, string grantType)
        {
            bool retVal = false;

            Client targetItem = this.ClientRepository.GetById(clientId);

            if (targetItem != null)
            {
                targetItem.RemoveGrantType(grantType);
                this.ClientRepository.Save(targetItem);
                retVal = true;
            }

            return retVal;
        }
    }
}
