using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth2.DataLayer.DTO
{
    public class AMFUser
    {
        /// <summary>
        /// Defines the Id field name for creating queries
        /// </summary>
        public const string IdFieldName = "Id";

        /// <summary>
        /// Defines the Email field name for creating queries
        /// </summary>
        public const string EmailFieldName = "Email";

        /// <summary>
        /// Default constructor for the class
        /// </summary>
        public AMFUser()
        {
            this.Id = 0;           
        }

        public long Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime DateCreated { get; set; }

        public string PasswordHint { get; set; }

        public int UserStatus { get; set; }

        public int Role { get; set; }

        public string ResetToken { get; set; }
    }
}
