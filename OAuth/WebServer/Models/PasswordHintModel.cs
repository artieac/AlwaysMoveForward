using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VP.Digital.Security.OAuth.WebServer.Models
{
    /// <summary>
    /// This is the model used for the Password Hint view
    /// </summary>
    public class PasswordHintModel
    {
        /// <summary>
        /// Gets or sets the password hint for the user.
        /// </summary>
        public string PasswordHint { get; set; }
    }
}