using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.Common.Security;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Web.Code;
using System.Security.Claims;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    /// <summary>
    /// A common base class for controllers
    /// </summary>
    public class MVCControllerBase : Controller
    {
        private AMFUserLogin currentUser;

        /// <summary>
        /// The service manager instance for the current controller context
        /// </summary>
        private IServiceManager serviceManager;

        /// <summary>
        /// Gets the current instance of the service manager
        /// </summary>
        public IServiceManager ServiceManager
        {
            get
            {
                if (serviceManager == null)
                {
                    serviceManager = ServiceManagerBuilder.CreateServiceManager();
                }

                return serviceManager;
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
                            this.currentUser = this.ServiceManager.UserService.GetUserById(int.Parse(claim.Value));
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