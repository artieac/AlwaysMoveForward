using System;
using ServiceStack.ServiceInterface;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using DevDefined.OAuth.Provider.Inspectors;
using DevDefined.OAuth.Framework.Signing;
using VP.Digital.Common.DataLayer.Configuration;
using VP.Digital.Common.Security;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.BusinessLayer.Services;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// A base class for Service Stack Component controllers
    /// </summary>
    public class ControllerBase : Service
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
                if (this.serviceManager == null)
                {
                    this.serviceManager = ServiceManagerBuilder.CreateServiceManager(DatabaseConfiguration.GetInstance());
                }

                return this.serviceManager;
            }
            set { this.serviceManager = value; }
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
            }
        }

        /// <summary>
        /// THe dev defined OAuth provider that does the majority of the work
        /// </summary>
        private OAuthProvider oauthProvider;

        /// <summary>
        /// Gets the current instance of the OAuth provider.
        /// </summary>
        protected OAuthProvider OAuthProvider
        {
            get
            {
                if (this.oauthProvider == null)
                {
                    this.oauthProvider = new OAuthProvider(this.ServiceManager.TokenService,
                                                            new SignatureValidationInspector(this.ServiceManager.ConsumerService),
                                                            new NonceStoreInspector(this.ServiceManager.ConsumerService),
                                                            new TimestampRangeInspector(new TimeSpan(1, 0, 0)),
                                                            new ConsumerValidationInspector(this.ServiceManager.ConsumerService),
                                                            new OAuth10AInspector(this.ServiceManager.TokenService));
                }

                return this.oauthProvider;
            }
        }      
    }
}
