using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Code
{
    public class ClaimsPrincipalFactory 
    {        public ClaimsPrincipal Create(AMFUserLogin userLogin)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("Password");
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.Name, userLogin.GetDisplayName()));
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.GivenName, userLogin.FirstName));
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.FamilyName, userLogin.LastName));
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.Email, userLogin.Email));
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.Role, userLogin.Role.ToString()));

            return new ClaimsPrincipal(claimsIdentity); 
        }
    }
}
