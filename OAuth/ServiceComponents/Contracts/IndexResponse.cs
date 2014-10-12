using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// The model returned to the Index page
    /// </summary>
    public class IndexResponse
    {
        /// <summary>
        /// Gets or sets the request token being authorized
        /// </summary>
        public string Token { get; set; }
    }
}
