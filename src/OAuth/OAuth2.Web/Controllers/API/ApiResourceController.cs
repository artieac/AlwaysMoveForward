using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
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
    public class ApiResourceController : AMFControllerBase
    {
        public ApiResourceController(ServiceManagerBuilder serviceManagerBuilder) 
                                : base(serviceManagerBuilder)
        {

        }        

        [Route("api/ApiResource/{id}"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ProtectedApiResource Get(long id)
        {
            ProtectedApiResource retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return retVal;
        }

        [Route("api/ApiResources"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public IList<ProtectedApiResource> GetAll()
        {
            IList<ProtectedApiResource> retVal = this.ServiceManager.ApiResourceService.GetAll();
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/ApiResource"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ProtectedApiResource Add([FromBody]ProtectedApiResource newResource)
        {
            return this.ServiceManager.ApiResourceService.Add(newResource.Name, newResource.DisplayName, newResource.Description, newResource.Enabled);
        }

        [Produces("application/json")]
        [Route("api/ApiResource/{id}/Secret"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ProtectedApiResource AddSecret(long id, [FromBody]SecretInputModel input)
        {
            ProtectedApiResource retVal = this.ServiceManager.ApiResourceService.AddSecret(id, input.Secret.Sha256(), ProtectedApiSecret.SecretEncryptionType.SHA256);
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/ApiResource/{id}/Secret/{secretId}"), HttpDelete()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public bool DeleteSecret(long id, long secretId)
        {
            this.ServiceManager.ApiResourceService.DeleteSecret(id, secretId);
            return this.ServiceManager.ApiResourceService.GetById(resourceId);
        }

        [Produces("application/json")]
        [Route("api/ApiResource/{id}/Claim"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ProtectedApiResource UpdateClaims(long id, [FromBody]ClaimInputModel input)
        {
            ProtectedApiResource retVal = this.ServiceManager.ApiResourceService.AddClaim(id, input.Value);
            return retVal;
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/ApiResource/{resourceId}/Claim/{claim}"), HttpDelete()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ProtectedApiResource DeleteClaim(long resourceId, string claim)
        {
            this.ServiceManager.ApiResourceService.DeleteClaim(resourceId, claim);
            return this.ServiceManager.ApiResourceService.GetById(resourceId);
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/ApiResource/{id}/Scope"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ProtectedApiResource UpdateScopes(long id, [FromBody]ScopeInputModel input)
        {
            ProtectedApiResource retVal = this.ServiceManager.ApiResourceService.AddScope(id, input.Name, input.Description);
            return retVal;
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/ApiResource/{resourceId}/Scope/{scopeId}"), HttpDelete()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ProtectedApiResource DeleteScope(long resourceId, long scopeId)
        {
            this.ServiceManager.ApiResourceService.DeleteScope(resourceId, scopeId);
            return this.ServiceManager.ApiResourceService.GetById(resourceId);
        }
    }
}
