using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Models.Account
{
    public class ProcessLoginInput
    {
        public string ReturnUrl { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
