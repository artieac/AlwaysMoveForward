using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel.Web;
using System.Web;
using System.Xml.Linq;
using DataServicesJSONP;

using log4net;

using AlwaysMoveForward.Common.Data;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.Core.Utilities;
using AnotherBlog.IntegrationService.BlogPosts.Requests;
using AnotherBlog.IntegrationService.BlogPosts.Responses;
using AnotherBlog.IntegrationService.Models;

namespace AnotherBlog.IntegrationService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [JSONPSupportBehavior]
    public class OData // : DataService //<AnotherBlogEntities>
    {
        IUnitOfWork unitOfWork;
        private ILog logger;
        private ServiceManager serviceManager;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (this.unitOfWork == null)
                {
                    this.unitOfWork = ServiceManager.CreateUnitOfWork();
                }

                return this.unitOfWork;
            }
        }

        public ServiceManager Services
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = ServiceManager.CreateServiceManager(this.UnitOfWork);
                }

                return this.serviceManager;
            }
        }

        public SecurityPrincipal CurrentPrincipal
        {
            get
            {
                SecurityPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal as SecurityPrincipal;

                if (currentPrincipal == null)
                {
                    try
                    {
                        currentPrincipal = new SecurityPrincipal(Services.Users.GetDefaultUser());
                        System.Threading.Thread.CurrentPrincipal = currentPrincipal;
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message, e);
                    }
                }

                return currentPrincipal;
            }
            set
            {
                System.Threading.Thread.CurrentPrincipal = value;
            }
        }

        public ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }

        public void DoWork()
        {
        }


    }
}
