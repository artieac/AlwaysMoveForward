using System;
using System.Collections.Generic;
using System.Linq;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Web.Models.API
{
    public class OAuthTokensModel
    {
        public OAuthTokensModel()
        {

        }

        /// <summary>
        /// Gets or sets the username to get history for.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the consumer key to get history for
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the search start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the search end date
        /// </summary>
        public DateTime EndDate { get; set;}
    }
}