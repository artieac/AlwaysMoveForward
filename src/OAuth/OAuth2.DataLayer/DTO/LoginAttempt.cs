using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.DTO
{
    public class LoginAttempt
    {
        /// <summary>
        /// Defines the Id field name for creating queries
        /// </summary>
        public const string IdFieldName = "Id";

        /// <summary>
        /// Defines the username field name for creating queries
        /// </summary>
        public const string UserNameField = "UserName";


        /// <summary>
        /// Gets or sets the Database id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the source of the login attempt
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the status of the login attempt
        /// </summary>
        public bool WasSuccessfull { get; set; }

        /// <summary>
        /// Gets or sets the Date of the login attempt
        /// </summary>
        public DateTime AttemptDate { get; set; }

        /// <summary>
        /// Gets or sets the user these login attempst are associated with
        /// </summary>
        public string UserName { get; set; }
    }
}
