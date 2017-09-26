using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.Web.Models
{
    /// <summary>
    /// A model to return the verifier code and granted flag to inline oauth consumers
    /// </summary>
    public class AuthorizeConsumerModel
    {
        /// <summary>
        /// The generated verifier code
        /// </summary>
        public string VerifierCode { get; set; }

        /// <summary>
        /// Wheterh or not access was granted
        /// </summary>
        public bool Granted { get; set; }
    }
}