using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DTO
{
    public class Consumer
    {
        /// <summary>
        /// A string to define the ConsumerKey field for creating queries
        /// </summary>
        public const string ConsumerKeyFieldName = "ConsumerKey";

        /// <summary>
        /// A string to define the email field name for creating queries
        /// </summary>
        public const string EmailFieldName = "ContactEmail";

        /// <summary>
        /// Gets or sets the consumer key
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the Public Key
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the consumer
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets if the access tokens should be auto granted
        /// </summary>
        public virtual bool AutoGrant { get; set; }

        /// <summary>
        /// Gets or sets how many hours before an access token expires for this consumer
        /// </summary>
        public virtual int AccessTokenLifetime { get; set; }
    }
}
