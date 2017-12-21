using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Controllers.API
{
    public class ScopeController : AMFControllerBase
    {
        public ScopeController(ServiceManagerBuilder serviceManagerBuilder) 
                                : base(serviceManagerBuilder)
        {

        }

        [Route("api/Scopes"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public IList<ProtectedApiScope> Get()
        {
            IList<ProtectedApiScope> retVal = this.ServiceManager.ApiResourceService.GetAvailableScopes();
            return retVal;
        }
    }
}
