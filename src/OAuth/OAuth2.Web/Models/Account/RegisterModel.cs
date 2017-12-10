using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.Web.Models.Account
{
    /// <summary>
    /// The class defines the inputs for the Register all
    /// </summary>
    public class RegisterModel
    {
        public string ReturnUrl { get; set; }
       
        public string ConsumerName { get; set; }

        public bool FoundUser { get; set; }
    }
}