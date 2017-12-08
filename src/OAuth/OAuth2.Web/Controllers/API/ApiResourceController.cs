using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using AlwaysMoveForward.OAuth2.Web.Models.API;
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
        public ApiResources Get(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return retVal;
        }

        [Route("api/ApiResources"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public IList<ApiResources> GetAll()
        {
            IList<ApiResources> retVal = this.ServiceManager.ApiResourceService.GetAll();
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/ApiResource"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ApiResources Add([FromBody]ApiResources newResource)
        {
            return this.ServiceManager.ApiResourceService.Add(newResource.Name, newResource.DisplayName, newResource.Description, newResource.Enabled);
        }

        [Produces("application/json")]
        [Route("api/ApiResource/{id}/Secret"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ApiResources UpdateSecrets(long id, [FromBody]SecretInputModel input)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.AddSecret(id, input.Secret);
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/ApiResource/{id}/Claim"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ApiResources UpdateClaims(long id, [FromBody]ClaimInputModel input)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.AddClaim(id, input.Value);
            return retVal;
        }

        [Produces("application/json")]
        [Route("api/ApiResource/{id}/Scope"), HttpPost()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public ApiResources UpdateScopes(long id, [FromBody]ApiResources newResource)
        {
            return this.ServiceManager.ApiResourceService.Update(newResource.Id, newResource.Name, newResource.DisplayName, newResource.Description, newResource.Enabled);
        }
    }
}
