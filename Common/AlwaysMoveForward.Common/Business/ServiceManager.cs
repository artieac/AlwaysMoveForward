using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class ServiceManager : ServiceRegisterBase
    {
        public static TService CreateService<TService>(ServiceContext serviceContext) where TService : class
        {
            return Activator.CreateInstance(typeof(TService),
                                                               new object[]
                                                               {
                                                                   serviceContext
                                                               }) as TService;
        }

        public static TServiceInterface CreateService<TServiceInterface, TService>(ServiceContext serviceContext) where TService : class, TServiceInterface where TServiceInterface : class
        {
            return Activator.CreateInstance(typeof(TService),
                                                               new object[]
                                                               {
                                                                   serviceContext
                                                               }) as TServiceInterface;
        }
        
        public ServiceManager(ServiceContext serviceContext)
        {
            this.RepositoryManager = serviceContext.RepositoryManager;
            this.UnitOfWork = serviceContext.UnitOfWork;
        }

        public IRepositoryManager RepositoryManager{ get; private set;}
        public IUnitOfWork UnitOfWork { get; private set;}

        public TServiceInterface ResolveOrRegister<TServiceInterface, TService>()
            where TServiceInterface : class
            where TService : class, TServiceInterface
        {
            TServiceInterface retVal = this.Resolve<TServiceInterface>();

            if (retVal == null)
            {
                TServiceInterface newService = Activator.CreateInstance(typeof(TService),
                                                               new object[]
                                                               {
                                                                   new ServiceContext(this.UnitOfWork, this.RepositoryManager)
                                                               }) as TServiceInterface;

                if (newService != null)
                {
                    retVal = this.RegisterService<TServiceInterface>(newService);
                }
            }

            return retVal;
        }
        public RoleService Roles
        {
            get{ return this.ResolveOrRegister<RoleService, RoleService>();}
        }

        public SiteInfoService SiteInfo
        {
            get { return this.ResolveOrRegister<SiteInfoService, SiteInfoService>(); }
        }

        public UserService UserService
        {
            get { return this.ResolveOrRegister<UserService, UserService>(); }
        }
    }
}
