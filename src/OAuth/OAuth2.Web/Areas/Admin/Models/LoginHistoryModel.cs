using System;
using System.Collections.Generic;
using System.Linq;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.Core.Common.Utilities;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Models
{
    /// <summary>
    /// This is a model with the user details
    /// </summary>
    public class LoginHistoryModel
    {
        public LoginHistoryModel()
        {
            this.LoginHistory = new PagedList<LoginAttempt>();
        }

        /// <summary>
        /// Gets or sets the username to get history for.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets and sets the login history associated with the user name
        /// </summary>
        public IPagedList<LoginAttempt> LoginHistory { get; set; }
    }
}