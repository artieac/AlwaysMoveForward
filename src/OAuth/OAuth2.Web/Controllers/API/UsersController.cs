using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using Microsoft.Extensions.Logging;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Web.Models;
using IdentityModel;

namespace AlwaysMoveForward.OAuth2.Web.Controllers.API
{
    public class UsersController : AMFControllerBase
    {
        public UsersController(ServiceManagerBuilder serviceManagerBuilder,
                                ILoggerFactory loggerFactory) 
                                : base(serviceManagerBuilder, loggerFactory.CreateLogger<UsersController>())
        {
            this.Logger = loggerFactory.CreateLogger<UsersController>();
        }

        [Route("api/User"), HttpGet()]
        [Authorize]
        public User Get()
        {
            User retVal = null;

            if (HttpContext.User != null)
            {               
                retVal = new User();
                retVal.Email = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email).Value;
                retVal.FirstName = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.GivenName).Value;
                retVal.LastName = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.FamilyName).Value;
                retVal.Id = long.Parse(this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject).Value);
            }

            return retVal;
        }

        public ILogger Logger { get; private set; }

        [Route("api/User/{id}"), HttpGet()]
        [Authorize]
        public User Get(long id)
        {
            User retVal = null;
            AlwaysMoveForward.OAuth2.Common.DomainModel.AMFUserLogin foundUser = this.ServiceManager.UserService.GetUserById(id);

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

            IList<User> retVal = this.ServiceManager.UserService.GetAll().Cast<User>().ToList();
            return new PagedListModel<AlwaysMoveForward.OAuth2.Common.DomainModel.User>(retVal, currentPageIndex);
        }

        [Route("api/Users/{emailAddress}"), HttpGet()]
        [Authorize]
        public IList<User> Get(string emailAddress)
        {
            IList<User> retVal = new List<User>();
            
            IList<AlwaysMoveForward.OAuth2.Common.DomainModel.AMFUserLogin> foundUsers = this.ServiceManager.UserService.SearchByEmail(emailAddress);

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
            this.ServiceManager.UserService.Delete(id);
        }
    }
}
