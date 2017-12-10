using AlwaysMoveForward.OAuth2.Common.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Models.Account
{
    public class EditModel
    {
        public EditModel(AMFUserLogin user)
        {
            this.Id = user.Id;
            this.UserEmail = user.Email;
            this.Password = "";
            this.PasswordHint = user.PasswordHint;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }

        /// <summary>
        /// Gets or sets the user Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user email that will be used for logging in
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password hint
        /// </summary>
        public string PasswordHint { get; set; }

        /// <summary>
        /// Gets or sets the users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the users last name
        /// </summary>
        public string LastName { get; set; }
    }
}
