using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Common.Security;
using VP.Digital.Security.OAuth.Contracts;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.WebServer.Code;

namespace VP.Digital.Security.OAuth.WebServer.Controllers
{
    /// <summary>
    /// A common base class for controllers
    /// </summary>
    public class ControllerBase : Controller
    {
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
        public OAuthServerSecurityPrincipal CurrentPrincipal
        {
            get
            {
                OAuthServerSecurityPrincipal retVal = System.Threading.Thread.CurrentPrincipal as OAuthServerSecurityPrincipal;

                if (retVal == null)
                {
                    try
                    {
                        retVal = new OAuthServerSecurityPrincipal(null);
                        System.Threading.Thread.CurrentPrincipal = retVal;
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger(this.GetType().Name).Error(e);
                    }
                }

                return retVal;
            }

            set
            {
                System.Threading.Thread.CurrentPrincipal = value;
                this.HttpContext.User = value;
            }
        }
    }
}