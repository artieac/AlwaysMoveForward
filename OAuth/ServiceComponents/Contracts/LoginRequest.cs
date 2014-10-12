using ServiceStack.ServiceHost;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// This object represents the parameters for the login page of the OAuth token authorization flow api
    /// </summary>
    [Route("/Authorization/Login")]
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the request token being authorized
        /// </summary>
        public string OAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the username being logged in
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for the user
        /// </summary>
        public string Password { get; set; }
    }
}
