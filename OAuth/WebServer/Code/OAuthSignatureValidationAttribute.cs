using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using VP.Digital.Common.Entities;
using VP.Digital.Common.Security;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.BusinessLayer;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.Common;
using VP.Digital.Security.OAuth.Contracts;
using VP.Digital.Security.OAuth.Contracts.Configuration;

namespace VP.Digital.Security.OAuth.WebServer.Code
{
    /// <summary>
    /// This attribute can be put on a web method to test the signature of an oauth signed request
    /// before getting into the method itself
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class OAuthSignatureValidationAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// The on authorization method is where the work happens
        /// </summary>
        /// <param name="authorizationContext">Wraps the request context.</param>
        public virtual void OnAuthorization(AuthorizationContext authorizationContext)
        {
            // Get the authentication cookie
            IServiceManager serviceManager = ServiceManagerBuilder.CreateServiceManager();
            OAuthSecurityPrincipal principal = null;

            var context = new HttpContextWrapper(HttpContext.Current);

            try
            {
                HttpRequestBase request = context.Request;

                string loadBalancerEndpointsValue = System.Configuration.ConfigurationManager.AppSettings[DigitalOAuthContextBuilder.LoadBalancerEndpointsSetting];
                string[] loadBalancerEndpoints = null;

                if (!string.IsNullOrEmpty(loadBalancerEndpointsValue))
                {
                    loadBalancerEndpoints = loadBalancerEndpointsValue.Split(',');
                }

                IOAuthContext oauthContext = DigitalOAuthContextBuilder.FromHttpRequest(request, loadBalancerEndpoints);

                ValidatedToken validatedToken = serviceManager.TokenService.ValidateSignature(oauthContext, oauthContext.Token, VP.Digital.Security.OAuth.Contracts.Constants.HmacSha1SignatureMethod, oauthContext.GenerateSignatureBase());

                if (validatedToken != null && validatedToken.User != null && validatedToken.OAuthToken != null)
                {
                    principal = new OAuthSecurityPrincipal(validatedToken.User, validatedToken.OAuthToken);
                    System.Threading.Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
                principal = null;
            }

            if (principal == null)
            {
                authorizationContext.Result = new RedirectResult(Constants.LoginRoute);
            }
        }
    }
}