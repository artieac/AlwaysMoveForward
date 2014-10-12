using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;
using ServiceStack.Common.Web;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// Exchange a verified request token for an access token
    /// </summary>
    public class ExchangeRequestTokenForAccessTokenController : ControllerBase
    {
        /// <summary>
        /// Takes the paramters as a signed OAuth request
        /// </summary>
        /// <param name="request">The request parameters</param>
        /// <returns>The granted access token and request</returns>
        public object Get(ExchangeRequestTokenForAccessTokenRequest request)
        {
            string retVal = string.Empty;

            try
            {
                IOAuthContext context = new ServiceStackOAuthContextBuilder().FromHttpRequest(Request);
                IToken accessToken = this.OAuthProvider.ExchangeRequestTokenForAccessToken(context);

                if (accessToken != null)
                {
                    retVal = string.Format("oauth_token={0}&oauth_token_secret={1}", accessToken.Token, accessToken.TokenSecret);
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            if (string.IsNullOrEmpty(retVal))
            {
                return new HttpResult(System.Net.HttpStatusCode.Unauthorized, "Not authorized");
            }
            else
            {
                return retVal;
            }
        }
    }
}