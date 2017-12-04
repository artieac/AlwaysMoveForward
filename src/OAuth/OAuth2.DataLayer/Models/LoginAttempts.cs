using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class LoginAttempts
    {
        public long Id { get; set; }
        public bool WasSuccessfull { get; set; }
        public DateTime AttemptDate { get; set; }
        public string Source { get; set; }
        public string UserName { get; set; }
    }
}
