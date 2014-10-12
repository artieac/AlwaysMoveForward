using ServiceStack.ServiceHost;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// This object represents the parameters for the initial login page
    /// </summary>
    [Route("/Authorization/Index")]
    public class IndexRequest
    {
        /// <summary>
        /// Gets or sets the request token being authorized
        /// </summary>
        public string oauth_token { get; set; }
    }
}
