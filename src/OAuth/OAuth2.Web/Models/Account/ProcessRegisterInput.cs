﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Models.Account
{
    public class ProcessRegisterInput
    {        
        /// <summary>
        /// Gets or sets the user Id
        /// </summary>
        public int Id { get; set; }

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

        public string ReturnUrl { get; set; }
    }
}