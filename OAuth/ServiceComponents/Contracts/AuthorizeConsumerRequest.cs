using ServiceStack.ServiceHost;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// This object represents the parameters for the AuthorizeConsumer api
    /// </summary>
    [Route("/OAuth/AuthorizeConsumer")]
    public class AuthorizeConsumerRequest
    {
        /// <summary>
        /// Gets or sets the request token being authorized
        /// </summary>
        public string oauth_token { get; set; }
    }
}
