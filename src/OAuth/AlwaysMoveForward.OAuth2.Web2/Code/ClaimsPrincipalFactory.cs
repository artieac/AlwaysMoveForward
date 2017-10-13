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
    {
        public IList<Claim> GenerateClaims(AMFUserLogin userLogin)
        {
            IList<Claim> retVal = new List<Claim>();
            retVal.Add(new Claim(JwtClaimTypes.Subject, userLogin.Id.ToString()));
            retVal.Add(new Claim(JwtClaimTypes.Name, userLogin.Email));
            retVal.Add(new Claim(JwtClaimTypes.GivenName, userLogin.FirstName));
            retVal.Add(new Claim(JwtClaimTypes.FamilyName, userLogin.LastName));
            retVal.Add(new Claim(JwtClaimTypes.Email, userLogin.Email));
            retVal.Add(new Claim(JwtClaimTypes.Role, userLogin.Role.ToString()));

            return retVal;
        }

        public ClaimsPrincipal Create(AMFUserLogin userLogin)
        {
            OAuthServerSecurityPrincipal securityPrincipal = new OAuthServerSecurityPrincipal(userLogin);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(securityPrincipal, this.GenerateClaims(userLogin), "Password", JwtClaimTypes.Name, JwtClaimTypes.Role);
            return new ClaimsPrincipal(claimsIdentity); 
        }
    }
}
