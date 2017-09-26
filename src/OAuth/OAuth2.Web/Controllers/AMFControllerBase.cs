using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    /// <summary>
    /// A common base class for controllers
    /// </summary>
    public class AMFControllerBase : Microsoft.AspNetCore.Mvc.Controller
    {
        public AMFControllerBase(ServiceManagerBuilder serviceManagerBuilder)
        {
            this.ServiceManagerBuilder = serviceManagerBuilder;
        }
        /// <summary>
        /// The service manager instance for the current controller context
        /// </summary>
        private ServiceManagerBuilder ServiceManagerBuilder { get; set; }

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
                    serviceManager = this.ServiceManagerBuilder.Create();
                }

                return serviceManager;
            }
        }
    }
}