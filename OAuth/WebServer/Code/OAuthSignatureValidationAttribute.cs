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
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Security;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.BusinessLayer;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Common;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Contracts.Configuration;

namespace AlwaysMoveForward.OAuth.WebServer.Code
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
            OAuthServerSecurityPrincipal principal = null;

            var context = new HttpContextWrapper(HttpContext.Current);

            try
            {
                HttpRequestBase request = context.Request;

                string loadBalancerEndpointsValue = System.Configuration.ConfigurationManager.AppSettings[AMFOAuthContextBuilder.LoadBalancerEndpointsSetting];
                string[] loadBalancerEndpoints = null;

                if (!string.IsNullOrEmpty(loadBalancerEndpointsValue))
                {
                    loadBalancerEndpoints = loadBalancerEndpointsValue.Split(',');
                }

                IOAuthContext oauthContext = AMFOAuthContextBuilder.FromHttpRequest(request, loadBalancerEndpoints);

                ValidatedToken validatedToken = serviceManager.TokenService.ValidateSignature(oauthContext, oauthContext.Token, AlwaysMoveForward.OAuth.Contracts.Constants.HmacSha1SignatureMethod, oauthContext.GenerateSignatureBase());

                if (validatedToken != null && validatedToken.User != null && validatedToken.OAuthToken != null)
                {
                    principal = new OAuthServerSecurityPrincipal(serviceManager.UserService.GetUserById(validatedToken.User.Id));
                    System.Threading.Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
                principal = null;
            }

            if (principal == null)
            {
                authorizationContext.Result = new RedirectResult(Constants.LoginRoute);
            }
        }
    }
}