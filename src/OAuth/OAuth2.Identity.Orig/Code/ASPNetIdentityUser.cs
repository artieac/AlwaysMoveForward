using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Code
{
    public class ASPNetIdentityUser : IdentityUser<long>
    {
        public ASPNetIdentityUser() : base() { } 
    }
}
