using System;
using System.Web.Security;
using ServiceStack.ServiceInterface;
using DevDefined.OAuth.Framework;
using VP.Digital.Common.Entities;
using VP.Digital.Common.Security;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.Common;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;
using ServiceStack.Common.Web;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// This class is for authorizing consumers that can autogrant access tokens.
    /// </summary>
    [DefaultView("AccessStatus")]
    public class AuthorizeConsumerController : ControllerBase
    {
        /// <summary>
        /// This call has to be a get due to how the OAuth spec works
        /// </summary>
        /// <param name="request">The request parameters</param>
        /// <returns></returns>
        public object Get(AuthorizeConsumerRequest request)
        {
            ApproveAccessResponse retVal = new ApproveAccessResponse() { Granted = false, VerifierCode = string.Empty };

            try
            {
                Consumer relatedConsumer = this.ServiceManager.ConsumerService.GetByRequestToken(request.oauth_token);

                if (relatedConsumer != null)
                {
                    if (relatedConsumer.AutoGrant == true)
                    {
                        RequestToken authorizedRequestToken = this.ServiceManager.TokenService.AutoGrantRequestToken(request.oauth_token, relatedConsumer);

                        if (authorizedRequestToken != null)
                        {
                            string callbackUrl = authorizedRequestToken.GenerateCallBackUrl();

                            if (!string.IsNullOrEmpty(callbackUrl) && !callbackUrl.StartsWith(VP.Digital.Security.OAuth.Contracts.Constants.InlineCallback))
                            {                               
                                return this.Redirect(callbackUrl);
                            }
                            else
                            {
                                return string.Format("{0}={1}&{2}={3}", VP.Digital.Security.OAuth.Contracts.Constants.VerifierCodeParameter, authorizedRequestToken.RequestTokenAuthorization.VerifierCode, VP.Digital.Security.OAuth.Contracts.Constants.GrantedAccessParameter, true);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            return new HttpResult(System.Net.HttpStatusCode.Unauthorized, "Not authorized");
        }
    }
}
