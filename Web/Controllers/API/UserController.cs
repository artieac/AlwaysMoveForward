using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Web.Code;

namespace AlwaysMoveForward.OAuth.Web.Controllers.API
{
    public class UserController : BaseAPIController
    {
        [Route("api/User"), HttpGet()]
        [WebAPIAuthorization]
        public User Search(string emailAddress)
        {
            User retVal = null;

            if (string.IsNullOrEmpty(emailAddress))
            {
                if (this.CurrentPrincipal.User != null)
                {
                    retVal = new User();
                    retVal.Email = this.CurrentPrincipal.User.Email;
                    retVal.FirstName = this.CurrentPrincipal.User.FirstName;
                    retVal.LastName = this.CurrentPrincipal.User.LastName;
                    retVal.Id = this.CurrentPrincipal.User.Id;
                }
            }
            else
            {
                retVal = this.Services.UserService.GetByEmail(emailAddress);
            }

            return retVal;
        }
    }
}
