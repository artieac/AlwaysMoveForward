using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace AlwaysMoveForward.OAuth2.Web.Controllers.API
{
    public class UsersController : BaseAPIController
    {
        [Route("api/User"), HttpGet()]
        [Authorize]
        public User Get()
        {
            User retVal = null;

            if (this.CurrentUser != null)
            {
                retVal = new User();
                retVal.Email = this.CurrentUser.Email;
                retVal.FirstName = this.CurrentUser.FirstName;
                retVal.LastName = this.CurrentUser.LastName;
                retVal.Id = this.CurrentUser.Id;
            }

            return retVal;
        }

        [Route("api/User/{id}"), HttpGet()]
        [Authorize]
        public User Get(long id)
        {
            User retVal = null;
            AlwaysMoveForward.OAuth2.Common.DomainModel.AMFUserLogin foundUser = this.Services.UserService.GetUserById(id);

            if(foundUser != null)
            {
                retVal = new User();
                retVal.Email = foundUser.Email;
                retVal.FirstName = foundUser.FirstName;
                retVal.LastName = foundUser.LastName;
                retVal.Id = foundUser.Id;
            }

            return retVal;
        }

        [Route("api/Users"), HttpGet()]
        [Authorize(Roles = RoleType.Names.Administrator)]
        public PagedListModel<User> GetAll(int? page)
        {
            int currentPageIndex = 0;

            if(page.HasValue)
            {
                currentPageIndex = page.Value - 1;
            }

            IList<User> retVal = this.Services.UserService.GetAll().Cast<User>().ToList();
            return new PagedListModel<AlwaysMoveForward.OAuth2.Common.DomainModel.User>(retVal, currentPageIndex);
        }

        [Route("api/Users/{emailAddress}"), HttpGet()]
        [Authorize]
        public IList<User> Get(string emailAddress)
        {
            IList<User> retVal = new List<User>();
            
            IList<AlwaysMoveForward.OAuth2.Common.DomainModel.AMFUserLogin> foundUsers = this.Services.UserService.SearchByEmail(emailAddress);

            for (int i = 0; i < foundUsers.Count; i++)
            {
                User mappedUser = new User();
                mappedUser.Email = foundUsers[i].Email;
                mappedUser.FirstName = foundUsers[i].FirstName;
                mappedUser.LastName = foundUsers[i].LastName;
                mappedUser.Id = foundUsers[i].Id;
                retVal.Add(mappedUser);
            }

            return retVal;
        }

        [Route("api/User/{id}"), HttpDelete()]
        [Authorize(Roles=RoleType.Names.Administrator)]
        public void Delete(long id)
        {
            this.Services.UserService.Delete(id);
        }
    }
}
