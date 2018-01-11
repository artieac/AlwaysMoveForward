using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Controllers.API
{
    public class GrantTypeController : AMFControllerBase
    {
        public GrantTypeController(ServiceManagerBuilder serviceManagerBuilder) 
                                : base(serviceManagerBuilder)
        {

        }

        [Route("api/GrantTypes"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public IList<string> Get()
        {
            IList<string> retVal = new List<string>();

            retVal.Add(GrantTypes.ClientCredentials.ElementAt(0));
            retVal.Add(GrantTypes.Code.ElementAt(0));
            retVal.Add(GrantTypes.Hybrid.ElementAt(0));
            retVal.Add(GrantTypes.Implicit.ElementAt(0));
            retVal.Add(GrantTypes.ResourceOwnerPassword.ElementAt(0));

            return retVal;
        }
    }
}