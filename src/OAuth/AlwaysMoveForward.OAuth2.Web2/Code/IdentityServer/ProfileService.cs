using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using System.Security.Claims;
using IdentityModel;

namespace AlwaysMoveForward.OAuth2.Web.Code.IdentityServer
{
    public class ProfileService : IProfileService
    {
        public ProfileService(ServiceManagerBuilder serviceManagerBuilder)
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

            if (foundUser != null)
            {
                IList<Claim> issuedClaims = new List<Claim>();

                foreach (string requestedClaim in context.RequestedClaimTypes)
                {
                    Claim newClaim = null;

                    switch (requestedClaim)
                    {
                        case JwtClaimTypes.Name:
                            newClaim = new Claim(JwtClaimTypes.Name, foundUser.GetDisplayName());
                            context.IssuedClaims.Add(newClaim);
                            issuedClaims.Add(newClaim);
                            break;
                        case JwtClaimTypes.Email:
                            newClaim = new Claim(JwtClaimTypes.Email, foundUser.Email);
                            context.IssuedClaims.Add(newClaim);
                            issuedClaims.Add(newClaim);
                            break;
                        case JwtClaimTypes.Role:
                            if (foundUser.IsInRole(RoleType.Names.Administrator))
                            {
                                newClaim = new Claim(JwtClaimTypes.Role, RoleType.Names.Administrator);
                                context.IssuedClaims.Add(newClaim);
                            }
                            else
                            {
                                newClaim = new Claim(JwtClaimTypes.Role, RoleType.Names.User);
                                context.IssuedClaims.Add(newClaim);
                            }
                            issuedClaims.Add(newClaim);
                            break;
                    }
                }

                context.AddRequestedClaims(issuedClaims);
            }

            return Task.FromResult(foundUser);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            bool retVal = false;
            AMFUserLogin foundUser = this.ServiceManager.UserService.GetByEmail(context.Caller);

            if(foundUser!=null)
            {
                if(foundUser.UserStatus == UserStatus.Active)
                {
                    retVal = true;
                }
            }

            return Task.FromResult(retVal);
        }
    }
}
