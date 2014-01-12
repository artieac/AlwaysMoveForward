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

        RoleService roleService;
        public RoleService RoleService
        {
            get
            {
                if(roleService==null)
                {
                    roleService = new RoleService(this.ServiceContext);
                }

                return roleService;
            }
        }

        SiteInfoService siteInfo;
        public SiteInfoService SiteInfoService
        {
            get
            {
                if (siteInfo == null)
                {
                    siteInfo = new SiteInfoService(this.ServiceContext);
                }

                return siteInfo;
            }
        }

        UserService userService;
        public UserService UserService
        {
            get
            {
                if (userService == null)
                {
                    userService = new UserService(this.ServiceContext);
                }

                return userService;
            }
        }
    }
}
