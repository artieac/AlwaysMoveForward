using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
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
    }
}
