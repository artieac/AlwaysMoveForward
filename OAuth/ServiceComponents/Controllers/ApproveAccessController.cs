using System;
using System.Web.Security;
using ServiceStack.ServiceInterface;
using DevDefined.OAuth.Framework;
using VP.Digital.Common.Entities;
using VP.Digital.Common.Security;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// This class approves an access token by granting a verifier code for it
    /// </summary>
    [DefaultView("AccessStatus")]
    public class ApproveAccessController: ControllerBase
    {
        /// <summary>
        /// Accept the paramters in a post request
        /// </summary>
        /// <param name="request">The request parameters</param>
        /// <returns>Either redirects to the callback url specified in the request token, or if no callback url it will show the verifier code on the screen</returns>
        public object Post(ApproveAccessRequest request)
        {
            ApproveAccessResponse retVal = new ApproveAccessResponse() { Granted = false, VerifierCode = string.Empty };

            try
            {
                System.Net.Cookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                DigitalUserLogin currentUser = this.ServiceManager.UserService.GetUserById(int.Parse(ticket.Name));

                if (currentUser != null)
                {
                    this.CurrentPrincipal = new OAuthServerSecurityPrincipal(currentUser);
                    RequestToken authorizedRequestToken = this.ServiceManager.TokenService.CreateVerifierAndAssociateUserInfo(request.OAuthToken, currentUser);

                    if(authorizedRequestToken != null)
                    {
                        string callbackUrl = authorizedRequestToken.GenerateCallBackUrl();
                        
                        if(!string.IsNullOrEmpty(callbackUrl))
                        {
                            return this.Redirect(callbackUrl);
                        }
                        else
                        {
                            retVal.VerifierCode = authorizedRequestToken.RequestTokenAuthorization.VerifierCode;
                            retVal.Granted = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            return retVal;
        }
    }
}
