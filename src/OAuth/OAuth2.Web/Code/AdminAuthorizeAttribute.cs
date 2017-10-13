using AlwaysMoveForward.OAuth2.Common.DomainModel;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Code
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public AdminAuthorizeAttribute()
        {

        }

        // This is temporary, for some reaosn the claims I add to the ClaimsPrincipal with roles is not
        // on the User object.  Need to figure this out.  For now.
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.IsInRole(RoleType.Names.Adminstrator))
            {

            }
        }
    }
}
