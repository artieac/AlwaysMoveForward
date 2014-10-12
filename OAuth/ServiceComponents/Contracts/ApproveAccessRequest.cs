using ServiceStack.ServiceHost;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// This object represents the parameters for the ApproveAccess api
    /// </summary>
    [Route("/Authorization/ApproveAccess")]
    public class ApproveAccessRequest
    {
        /// <summary>
        /// Gets or sets the OAuth token to approve
        /// </summary>
        public string OAuthToken { get; set; }
    }
}
