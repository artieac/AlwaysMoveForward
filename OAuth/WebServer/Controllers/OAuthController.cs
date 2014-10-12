using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.Contracts;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.BusinessLayer;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.WebServer.Models;
using VP.Digital.Security.OAuth.WebServer.Code;

namespace VP.Digital.Security.OAuth.WebServer.Controllers
{
    /// <summary>
    /// The main interface for the OAuth clients
    /// </summary>
    public class OAuthController : ControllerBase
    {
        /// <summary>
        /// THe dev defined OAuth provider that does the majority of the work
        /// </summary>
        private OAuthProvider oauthProvider;

        /// <summary>
        /// Gets the current instance of the OAuth provider.
        /// </summary>
        /// <returns>An instance of a DevDefined OAuth provider</returns>
        private OAuthProvider OAuthProvider
        {
            get
            {
                if (this.oauthProvider == null)
                {
                    this.oauthProvider = OAuthServiceProviderFactory.GetServiceProvider(this.ServiceManager.TokenService, this.ServiceManager.ConsumerService);
                }

                return this.oauthProvider;            
            }
        }

        /// <summary>
        ///  Determine if the oauth proxy is running behind a load balancer and then create the context.
        /// </summary>
        /// <returns>An OAuth context instance</returns>
        private IOAuthContext GetContextBuilder()
        {
            string loadBalancerEndpointsValue = System.Configuration.ConfigurationManager.AppSettings[DigitalOAuthContextBuilder.LoadBalancerEndpointsSetting];
            string[] loadBalancerEndpoints = null;

            if (!string.IsNullOrEmpty(loadBalancerEndpointsValue))
            {
                loadBalancerEndpoints = loadBalancerEndpointsValue.Split(',');
            }

            return DigitalOAuthContextBuilder.FromHttpRequest(this.Request, loadBalancerEndpoints);
        }

        /// <summary>
        /// Establishes a new consumer and returns that information to the caller
        /// </summary>
        /// <param name="consumerName">The user friendly name to associate with this consumer</param>
        /// <param name="contactEmail">The contact email for this consumer</param>
        /// <returns>A new consumer</returns>
        [HttpPost]
        public ActionResult Consumer(string consumerName, string contactEmail)
        {
            Consumer newConsumer = this.ServiceManager.ConsumerService.Create(consumerName, contactEmail);

            TokenModel model = new TokenModel();
            model.Token = newConsumer.ConsumerKey;
            model.Secret = newConsumer.ConsumerSecret;

            return this.View(model);
        }

        /// <summary>
        /// Creates a new request token for a given consumer and returns it to the caller
        /// </summary>
        /// <returns>The request token and secret</returns>
        public ActionResult GetRequestToken()
        {
            TokenModel model = new TokenModel();

            try
            {
                LogManager.GetLogger(this.GetType()).Debug("GetRequestToken-Host=" + this.Request.Headers["Host"]);
                LogManager.GetLogger(this.GetType()).Debug("GetRequestToken-Authorization=" + this.Request.Headers[VP.Digital.Security.OAuth.Contracts.Constants.AuthorizationHeader]);

                IOAuthContext context = this.GetContextBuilder();
                string requestTokenContext = "GetRequestToken for " + context.ConsumerKey + ":RequestUrl=" + this.Request.Url + ":Realm=" + context.Realm + ":CallbackUrl=" + context.CallbackUrl + ":SignatureBase=" + context.GenerateSignatureBase();
                LogManager.GetLogger(this.GetType()).Debug(requestTokenContext);
                IToken requestToken = this.OAuthProvider.GrantRequestToken(context);

                model.Token = requestToken.Token;
                model.Secret = requestToken.TokenSecret;

                LogManager.GetLogger(this.GetType()).Debug(requestTokenContext + ":Token=" + model.Token);
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            return this.View(model);
        }

        /// <summary>
        /// Create a new access token for a given request token
        /// </summary>
        /// <returns>The Access Token and secret</returns>
        public ActionResult ExchangeRequestTokenForAccessToken()
        {
            TokenModel model = new TokenModel();

            try
            {
                IOAuthContext context = this.GetContextBuilder();
                IToken accessToken = this.OAuthProvider.ExchangeRequestTokenForAccessToken(context);

                model.Token = accessToken.Token;
                model.Secret = accessToken.TokenSecret;
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            if (string.IsNullOrEmpty(model.Token))
            {
                return new HttpUnauthorizedResult();
            }
            else
            {
                return this.View(model);
            }
        }

        /// <summary>
        /// Retrive a specific Access Token
        /// </summary>
        /// <param name="oauth_token">The access token to retrieve</param>
        /// <returns>The Access token and secret</returns>
        public ActionResult GetAccessToken()
        {
            TokenModel model = new TokenModel();

            try
            {
                IOAuthContext context = this.GetContextBuilder();
                IToken requestToken = this.ServiceManager.TokenService.GetValidAccessTokenByRequestToken(context.Token, context.Verifier);

                if (requestToken != null)
                {
                    model.Token = requestToken.Token;
                    model.Secret = requestToken.TokenSecret;
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            return this.View(model);
        }

        /// <summary>
        /// This method is used for inline OAuth authorization.  The only consumers that will be able to do inline oauth authorization will be the
        /// ones that have been pre configured to allow auto grants.
        /// </summary>
        /// <param name="oauth_token">the request token being authorized</param>
        /// <returns>the verifier code</returns>
        public ActionResult AuthorizeConsumer(string oauth_token)
        {
            AuthorizeConsumerModel model = new AuthorizeConsumerModel();

            try
            {
                string logMessageBase = "AuthorizeConsumer for:" + oauth_token;
                LogManager.GetLogger(this.GetType()).Debug(logMessageBase);

                Consumer relatedConsumer = this.ServiceManager.ConsumerService.GetByRequestToken(oauth_token);

                if (relatedConsumer != null)
                {
                    if (relatedConsumer.AutoGrant == true)
                    {
                        LogManager.GetLogger(this.GetType()).Debug(logMessageBase + ":Autogrant=true");
                        RequestToken authorizedRequestToken = this.ServiceManager.TokenService.AutoGrantRequestToken(oauth_token, relatedConsumer);

                        if (authorizedRequestToken != null)
                        {
                            LogManager.GetLogger(this.GetType()).Debug(logMessageBase + ":AuthorizedToken:VerifierCode=" + authorizedRequestToken.RequestTokenAuthorization.VerifierCode);

                            string callbackUrl = authorizedRequestToken.GenerateCallBackUrl();

                            if (!string.IsNullOrEmpty(callbackUrl) && !callbackUrl.StartsWith(VP.Digital.Security.OAuth.Contracts.Constants.InlineCallback))
                            {
                                LogManager.GetLogger(this.GetType()).Debug(logMessageBase + ":RedirectingTo=" + callbackUrl);
                                return this.Redirect(callbackUrl);
                            }
                            else
                            {
                                LogManager.GetLogger(this.GetType()).Debug(logMessageBase + ":ReturnToCaller");
                                model.VerifierCode = authorizedRequestToken.RequestTokenAuthorization.VerifierCode;
                                model.Granted = true;
                                return this.View(model);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            return new HttpUnauthorizedResult();
        }
    }
}
