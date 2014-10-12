using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// This object represents the parameters returned by the ApproveAccess api
    /// </summary>
    public class ApproveAccessResponse
    {
        /// <summary>
        /// Gets or sets if access was granted
        /// </summary>
        public bool Granted { get; set; }

        /// <summary>
        /// Gets or sets the reqeust token verification code
        /// </summary>
        public string VerifierCode { get; set; }
    }
}
