using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IdentityModel;
using AlwaysMoveForward.OAuth2.Client.Code;
using IdentityModel.Client;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace OAuth2.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> StartAuthentication()

        {

            // read discovery document to find authorize endpoint

            var authorizeUrl = new AuthorizeRequest(Constants.Authority + "/connect/authorize").CreateAuthorizeUrl(

                clientId: "abcd",

                responseType: "id_token token",

                scope: "api1.full_access openid",

                redirectUri: "http://localhost:53109/home/callback",

                state: CryptoRandom.CreateUniqueId(),
                nonce: CryptoRandom.CreateUniqueId(),
                responseMode: "form_post");



            return Redirect(authorizeUrl);

        }

        public async Task<IActionResult> Callback()
        {

            var state = Request.Form["state"].FirstOrDefault();

            var idToken = Request.Form["id_token"].FirstOrDefault();

            var error = Request.Form["error"].FirstOrDefault();



            if (!string.IsNullOrEmpty(error)) throw new Exception(error);

//            if (!string.Equals(state, "random_state")) throw new Exception("invalid state");



            var user = await ValidateIdentityToken(idToken);



            await HttpContext.Authentication.SignInAsync("Cookies", user);

            return Redirect("/home/secure");

        }



        private async Task<ClaimsPrincipal> ValidateIdentityToken(string idToken)

        {

            // read discovery document to find issuer and key material

            var disco = await DiscoveryClient.GetAsync(Constants.Authority);



            var keys = new List<SecurityKey>();

            foreach (var webKey in disco.KeySet.Keys)

            {

                var e = Base64Url.Decode(webKey.E);

                var n = Base64Url.Decode(webKey.N);



                var key = new RsaSecurityKey(new RSAParameters { Exponent = e, Modulus = n });

                key.KeyId = webKey.Kid;



                keys.Add(key);

            }



            var parameters = new TokenValidationParameters

            {

                ValidIssuer = disco.TryGetString(OidcConstants.Discovery.Issuer),

                ValidAudience = "mvc.manual",

                IssuerSigningKeys = keys,



                NameClaimType = JwtClaimTypes.Name,

                RoleClaimType = JwtClaimTypes.Role

            };



            var handler = new JwtSecurityTokenHandler();

            handler.InboundClaimTypeMap.Clear();



            SecurityToken token;

            var user = handler.ValidateToken(idToken, parameters, out token);



            var nonce = user.FindFirst("nonce")?.Value ?? "";

            if (!string.Equals(nonce, "random_nonce")) throw new Exception("invalid nonce");



            return user;

        }

    }
}
