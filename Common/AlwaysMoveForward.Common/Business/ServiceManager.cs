using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class ServiceManager
    {        
        public ServiceManager(ServiceContext serviceContext)
        {
            this.ServiceContext = serviceContext;
        }

        public ServiceContext ServiceContext { get; private set; }
        
        public IRepositoryManager RepositoryManager
        {
            get { return this.ServiceContext.RepositoryManager; }
        }

        public IUnitOfWork UnitOfWork 
        {
            get { return this.ServiceContext.UnitOfWork; }
        }

        private SiteInfoService siteInfo;
        public SiteInfoService SiteInfoService
        {
            get
            {
                if (this.siteInfo == null)
                {
                    this.siteInfo = new SiteInfoService(this.ServiceContext);
                }

                return this.siteInfo;
            }
        }

        private UserService userService;
        public UserService UserService
        {
            get
            {
                if (this.userService == null)
                {
                    this.userService = new UserService(this.UnitOfWork, this.RepositoryManager.Users);
                }

                return this.userService;
            }
        }
    }
}
