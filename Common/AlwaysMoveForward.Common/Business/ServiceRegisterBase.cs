using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ninject;

using AlwaysMoveForward.Common.Configuration;

namespace AlwaysMoveForward.Common.Business
{
    public abstract class ServiceRegisterBase
    {
        protected static IKernel kernel;

        public static IKernel InitializeDependencyInjection()
        {
            return ServiceManagerBase.InitializeDependencyInjection(RepositoryConfiguration.k_DefaultConfiguration);
        }

        public static IKernel InitializeDependencyInjection(String configurationSection)
        {
            if (kernel == null)
            {
                // Create Ninject DI Kernel
                kernel = new StandardKernel();

                // Register services with our Ninject DI Container
                RepositoryConfiguration configuration = (RepositoryConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
                kernel.Load(configuration.ManagerAssembly);
            }

            return kernel;
        }

        public ServiceRegisterBase() { }

        private IDictionary<Type, object> serviceContainer = new Dictionary<Type, object>();

        public TService RegisterService<TService>(TService service) where TService : class
        {
            return this.RegisterService<TService>(typeof(TService), service);
        }

        public TService RegisterService<TService>(Type serviceType, TService service) where TService : class
        {
            TService retVal = null;

            if (!serviceContainer.ContainsKey(serviceType))
            {
                serviceContainer.Add(serviceType, service);
                retVal = service;
            }

            return retVal;
        }

        public TService Resolve<TService>() where TService : class
        {
            TService retVal = null;
            Type serviceType = typeof(TService);

            if (serviceContainer.ContainsKey(serviceType))
            {
                retVal = serviceContainer[serviceType] as TService;
            }

            return retVal;
        }
    }
}
