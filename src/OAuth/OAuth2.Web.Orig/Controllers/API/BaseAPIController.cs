using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlwaysMoveForward.OAuth2.Web.Controllers.API
{
    public class BaseAPIController : ControllerBase
    {
        private AMFUserLogin currentUser;

        private IServiceManager serviceManager;

        public IServiceManager Services
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = ServiceManagerBuilder.CreateServiceManager();
                }

                return this.serviceManager;
            }
        }

        /// <summary>
        /// Gets or sets the current IPrincipal off of, or onto, the thread
        /// </summary>
        public AMFUserLogin CurrentUser
        {
            get
            {
                if (this.currentUser == null)
                {

                    ClaimsPrincipal principal = ClaimsPrincipal.Current;

                    foreach (Claim claim in principal.Claims)
                    {
                        if (claim.Type == ClaimTypes.Role)
                        {
                            this.currentUser = this.Services.UserService.GetUserById(int.Parse(claim.Value));
                        }
                    }
                }

                return this.currentUser;
            }
            set
            {
                this.currentUser = value;
            }
        }
    }
}