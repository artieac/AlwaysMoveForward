using System;
using DevDefined.OAuth.Framework;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// A controller to handle the GetRequestToken request
    /// </summary>
    public class GetRequestTokenController : ControllerBase
    {
        /// <summary>
        /// Gets a new reqeust token for a given consumer
        /// </summary>
        /// <param name="request">The parsed request parameters</param>
        /// <returns>The request token in the html body</returns>
        public object Get(GetRequestTokenRequest request)
        {
            string retVal = string.Empty;

            try
            {
                IOAuthContext context = new ServiceStackOAuthContextBuilder().FromHttpRequest(this.Request);
                IToken requestToken = this.OAuthProvider.GrantRequestToken(context);

                retVal = string.Format("oauth_token={0}&oauth_token_secret={1}&oauth_callback_confirmed=true", requestToken.Token, requestToken.TokenSecret);
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            return retVal;
        }
    }
}
