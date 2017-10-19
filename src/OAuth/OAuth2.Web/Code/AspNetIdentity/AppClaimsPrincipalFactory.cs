using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Code.AspNetIdentity
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<AMFUserLogin, string>
    {
        public AppClaimsPrincipalFactory(
            UserManager<AMFUserLogin> userManager,
            RoleManager<string> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
        }

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

        public async override Task<ClaimsPrincipal> CreateAsync(AMFUserLogin user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(this.GenerateClaims(user));

            return principal;
        }
    }
}
