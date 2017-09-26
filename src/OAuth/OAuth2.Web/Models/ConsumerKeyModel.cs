using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.Web.Models
{
    /// <summary>
    /// A model for returning the consumer information
    /// </summary>
    public class ConsumerKeyModel
    {
        /// <summary>
        /// Gets or sets the consumer key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret
        /// </summary>
        public string Secret { get; set; }
        
    }
}