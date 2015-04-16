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
    public class UsersController : BaseAPIController
    {
        [WebAPIAuthorization]
        public User Get()
        {
            User retVal = null;

            if (this.CurrentPrincipal.User != null)
            {
                retVal = new User();
                retVal.Email = this.CurrentPrincipal.User.Email;
                retVal.FirstName = this.CurrentPrincipal.User.FirstName;
                retVal.LastName = this.CurrentPrincipal.User.LastName;
                retVal.Id = this.CurrentPrincipal.User.Id;
            }

            return retVal;
        }

        [WebAPIAuthorization]
        public User Get(long id)
        {
            return this.Services.UserService.GetUserById(id);
        }

        [WebAPIAuthorization]
        public User Get(string emailAddress)
        {
            return this.Services.UserService.GetByEmail(emailAddress);
        }
    }
}
