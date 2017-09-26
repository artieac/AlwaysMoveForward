using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.Encryption;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel
{
    /// <summary>
    /// An extension of uSER to allow for storing of a password
    /// </summary>
    public class AMFUserLogin : User
    {
        /// <summary>
        /// Defines how many login failures are allowed before locking the account
        /// </summary>
        public const int MaxAllowedLoginFailures = 10;

        public const int SaltIterations = 1000;

        /// <summary>
        /// Defines how long to lock the user out for after failed login attempts
        /// </summary>
        public static double AccountLockTimeout = 30;


        /// <summary>
        /// Initialize id so that it is marked as unsaved.
        /// </summary>
        public AMFUserLogin()
        {
            this.Id = 0;
            this.DateCreated = DateTime.UtcNow;
            this.UserStatus = UserStatus.Active;
            this.Role = RoleType.Id.User;
        }

        public string GenerateNewPassword()
        {
            string retVal = string.Empty;
            Random random = new Random();
            string legalChars = "abcdefghijklmnopqrstuvwxzyABCDEFGHIJKLMNOPQRSTUVWXZY1234567890";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(legalChars.Substring(random.Next(0, legalChars.Length - 1), 1));
            }

            retVal = sb.ToString();

            this.UpdatePassword(retVal);

            return retVal;
        }
        
        public void UpdatePassword(string unencryptedPassword)
        {
            SHA1HashUtility passwordHashUtility = new SHA1HashUtility();
            this.PasswordHash = passwordHashUtility.HashPassword(unencryptedPassword);
            this.PasswordSalt = Convert.ToBase64String(passwordHashUtility.Salt);
        }

        public void UpdatePassword(string passwordHash, string passwordSalt)
        {
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
        }
        /// <summary>
        /// The salt associated with the hashed password
        /// </summary>
        public string PasswordSalt { get; private set; }

        /// <summary>
        /// Gets or sets the actually hashed password
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Gets or sets the date time that the user is created
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets or sets the forgotten password hint
        /// </summary>
        public string PasswordHint { get; set; }

        /// <summary>
        /// Gets the current status of the user
        /// </summary>
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// Gets or sets the current user role
        /// </summary>
        public RoleType.Id Role { get; set; }

        public bool IsInRole(string roleName)
        {
            RoleType.Id targetRole = RoleType.RolesByName[roleName];

            return this.IsInRole(targetRole);
        }

        public bool IsInRole(RoleType.Id roleType)
        {
            bool retVal = false;

            if(this.Role == roleType)
            {
                retVal = true;
            }

            return retVal;
        }
    }
}
