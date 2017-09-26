using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using System.Security.Claims;

namespace AlwaysMoveForward.OAuth2.Web.Code
{
    public class AMFProfileService : IProfileService
    {
        public AMFProfileService(ServiceManagerBuilder serviceManagerBuilder)
        {
            this.ServiceManagerBuilder = serviceManagerBuilder;
        }

        private ServiceManagerBuilder ServiceManagerBuilder { get; set; }

        private IServiceManager serviceManager = null;

        private IServiceManager ServiceManager
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = this.ServiceManagerBuilder.Create();
                }

                return this.serviceManager;
            }
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            AMFUserLogin foundUser = null;

            if(context.Subject.Identity != null)
            {
                foundUser = this.ServiceManager.UserService.GetByEmail(context.Subject.Identity.Name);
            }
            else
            {
                foreach (ClaimsIdentity identity in context.Subject.Identities)
                {
                    foundUser = this.ServiceManager.UserService.GetByEmail(identity.Name);

                    if (foundUser != null)
                    {
                        break;
                    }
                }
            }

            return Task.FromResult(foundUser);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            bool retVal = false;
            AMFUserLogin foundUser = this.ServiceManager.UserService.GetByEmail(context.Caller);

            if(foundUser!=null)
            {
                retVal = true;
            }

            return Task.FromResult(retVal);
        }
    }
}
