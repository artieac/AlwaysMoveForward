using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.Entities;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// An extension of DigitalUser to allow for storing of a password
    /// </summary>
    public class DigitalUserLogin : DigitalUser
    {
        /// <summary>
        /// Defines how many login failures are allowed before locking the account
        /// </summary>
        public const int MaxAllowedLoginFailures = 10;
        
        /// <summary>
        /// Defines how long to lock the user out for after failed login attempts
        /// </summary>
        public static double AccountLockTimeout = 30;

        /// <summary>
        /// Initialize id so that it is marked as unsaved.
        /// </summary>
        public DigitalUserLogin()
        {
            this.Id = 0;
            this.DateCreated = DateTime.UtcNow;
            this.UserStatus = DigitalUserStatus.Active;
            this.Role = OAuthRoles.User;
        }

        /// <summary>
        /// The salt associated with the hashed password
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets how many iterations were done to get the hash
        /// </summary>
        public int SaltIterations { get; set; }

        /// <summary>
        /// Gets or sets the actually hashed password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the date time that the user is created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the forgotten password hint
        /// </summary>
        public string PasswordHint { get; set; }

        /// <summary>
        /// Gets the current status of the user
        /// </summary>
        public DigitalUserStatus UserStatus { get; set; }

        /// <summary>
        /// Gets or sets the current user role
        /// </summary>
        public OAuthRoles Role { get; set; }
    }
}
