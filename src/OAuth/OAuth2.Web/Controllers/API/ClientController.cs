using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using ConsumerManagement = AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using AlwaysMoveForward.OAuth2.Web.Models.API;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Controllers.API
{
    public class ClientController : AMFControllerBase
    {
        public ClientController(ServiceManagerBuilder serviceManagerBuilder) 
                                : base(serviceManagerBuilder)
        {

        }

        [Route("api/Client/{id}"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client Get(long id)
        {
            ConsumerManagement.Client retVal = this.ServiceManager.ClientService.GetById(id);
            return retVal;
        }

        [Route("api/Clients"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public IList<ConsumerManagement.Client> GetAll()
        {
            IList<ConsumerManagement.Client> retVal = this.ServiceManager.ClientService.GetAll();
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/Client"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client Add([FromBody]ConsumerManagement.Client newResource)
        {
            if(newResource.Id > 0)
            {
                return this.ServiceManager.ClientService.Update(newResource.Id, newResource.ClientName, newResource.ClientName, newResource.Description, newResource.Enabled);
            }
            else
            {
                return this.ServiceManager.ClientService.Add(newResource.ClientName, newResource.ClientName, newResource.Description, newResource.Enabled);
            }
        }

        [Produces("application/json")]
        [Route("api/Client/{id}/Secret"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client AddSecret(long id, [FromBody]SecretInputModel input)
        {
            ConsumerManagement.Client retVal = this.ServiceManager.ClientService.AddSecret(id, input.Secret.Sha256(), ProtectedApiSecret.SecretEncryptionType.SHA256, "");
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/Client/{id}/Secret/{secretId}"), HttpDelete()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public bool DeleteSecret(long id, int secretId)
        {
            this.ServiceManager.ClientService.DeleteSecret(id, secretId);
            return true;
        }

        [Produces("application/json")]
        [Route("api/Client/{id}/RedirectUri"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client AddRedirectUri(long id, [FromBody]ClaimInputModel input)
        {
            ConsumerManagement.Client retVal = this.ServiceManager.ClientService.AddRedirectUri(id, input.Value);
            return retVal;
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/Client/{resourceId}/RedirectUri/{redirectUriId}"), HttpDelete()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client DeleteClaim(long resourceId, int redirectUriId)
        {
            this.ServiceManager.ClientService.DeleteRedirectUri(resourceId, redirectUriId);
            return this.ServiceManager.ClientService.GetById(resourceId);
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/Client/{id}/Scopes"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client UpdateScopes(long id, [FromBody]UpdateScopesModel input)
        {
            ConsumerManagement.Client retVal = this.ServiceManager.ClientService.UpdateScopes(id, input.Scopes);
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/Client/{clientId}/GrantType"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client UpdateClaims(long clientId, [FromBody]ClaimInputModel input)
        {
            ConsumerManagement.Client retVal = this.ServiceManager.ClientService.AddGrantType(clientId, input.Value);
            return retVal;
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/Client/{clientId}/GrantType/{grantType}"), HttpDelete()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ConsumerManagement.Client DeleteClaim(long clientId, string grantType)
        {
            this.ServiceManager.ClientService.DeleteGrantType(clientId, grantType);
            return this.ServiceManager.ClientService.GetById(clientId);
        }

    }
}
