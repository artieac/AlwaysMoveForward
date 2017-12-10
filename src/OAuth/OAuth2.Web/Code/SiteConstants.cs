using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Code
{
    public class SiteConstants
    {
        public const string AuthenticationScheme = IdentityServer4.IdentityServerConstants.DefaultCookieAuthenticationScheme;
//        public const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        public class PageLocations
        {
            public const string AdminHome = "/Admin/Management/Index";
        }
    }
}
