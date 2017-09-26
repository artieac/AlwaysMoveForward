using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityServer4;

namespace AlwaysMoveForward.OAuth2.Web.Code
{
    public class AMFClientStore : IClientStore
    {
        public AMFClientStore(ServiceManagerBuilder serviceManagerBuilder)
        {
            this.ServiceManagerBuilder = serviceManagerBuilder;
        }

        private ServiceManagerBuilder ServiceManagerBuilder { get; set;  }

        private IServiceManager serviceManager = null;

        private IServiceManager ServiceManager
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = this.ServiceManagerBuilder.Create();
                }

                return this.serviceManager;
            }
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            Client retVal = null;

            Consumer foundItem = this.ServiceManager.ConsumerService.GetConsumer(clientId);

            if(foundItem != null)
            {
                retVal = new Client();
                retVal.AbsoluteRefreshTokenLifetime = foundItem.AccessTokenLifetime;
                retVal.AccessTokenLifetime = foundItem.AccessTokenLifetime;
                retVal.ClientId = clientId;
                retVal.ClientName = foundItem.Name;

                retVal.RedirectUris = new List<string>();
                retVal.RedirectUris.Add("http://localhost:53109/home/callback");

                retVal.LogoutUri = "http://localhost:53109/home/logout";

                retVal.PostLogoutRedirectUris = new List<string>();
                retVal.PostLogoutRedirectUris.Add("http://localhost:53109/home/logout");

                retVal.AllowedGrantTypes = GrantTypes.Implicit;
                retVal.AllowAccessTokensViaBrowser = true;

                retVal.AllowedScopes = new List<string>();
                retVal.AllowedScopes.Add(IdentityServerConstants.StandardScopes.OpenId);
                retVal.AllowedScopes.Add(IdentityServerConstants.StandardScopes.Profile);
                retVal.AllowedScopes.Add(IdentityServerConstants.StandardScopes.Email);
                retVal.AllowedScopes.Add("api1");
                retVal.AllowedScopes.Add("api1.full_access");
                retVal.AllowedScopes.Add("api1.read_only");
            }

            return Task.FromResult(retVal);
        }
    }
}
