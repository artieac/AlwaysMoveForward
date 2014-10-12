using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// The model for the login page
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the reqeust token being authorized
        /// </summary>
        public string OAuthToken { get; set; }
    }
}
