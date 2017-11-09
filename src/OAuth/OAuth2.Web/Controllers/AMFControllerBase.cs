﻿using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _loggerFactory;

        public AMFControllerBase(ServiceManagerBuilder serviceManagerBuilder, ILogger logger)
        {
            this.ServiceManagerBuilder = serviceManagerBuilder;
            this.Logger = logger;
        }
        /// <summary>
        /// The service manager instance for the current controller context
        /// </summary>
        private ServiceManagerBuilder ServiceManagerBuilder { get; set; }

        private IServiceManager serviceManager;

        public ILogger Logger { get; private set; }

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