using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.Common.Configuration;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.Security;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class UserController : MVCControllerBase
    {        
        public ActionResult EmbeddedSignin()
        {
            return this.View();
        }

        /// <summary>
        /// Show the initial sign in page
        /// </summary>
        /// <param name="oauth_Token">The oauth token that this sign in is trying to authorize.</param>
        /// <returns>The index view</returns>
        public ActionResult Signin(string oauth_Token)
        {
            TokenModel model = new TokenModel() { Token = oauth_Token };

            Consumer consumer = this.ServiceManager.ConsumerService.GetByRequestToken(oauth_Token);

            if (consumer != null)
            {
                model.ConsumerName = consumer.Name;
            }

            return this.View("Signin", model);
        }

        /// <summary>
        /// Returns the Signup view for registration.
        /// </summary>
        /// <returns>The MVC View for a registaration</returns>
        public ActionResult SignUp(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View(model);
        }

        public ClaimsIdentity GenerateIdentityFromUser(AMFUserLogin user)
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, RoleType.Id.Administrator.ToString()));

            ClaimsIdentity retVal = new ClaimsIdentity(claims);
            return retVal;
        }

        /// <summary>
        /// Setup the logged in user on the current thread and setup an auth cookie.
        /// </summary>
        /// <param name="user"></param>
        private void SetCurrentUser(AMFUserLogin user)
        {
            ClaimsIdentity newIdentity = this.GenerateIdentityFromUser(user);
            ClaimsPrincipal.Current.AddIdentity(newIdentity);

            HttpContext.Authentication.SignInAsync("AMFCookieMiddlewareInstance", ClaimsPrincipal.Current);
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        public ActionResult Register(RegisterModel registerModel)
        {
            AMFUserLogin registeredUser = null;

            if (registerModel != null)
            {
                registeredUser = this.ServiceManager.UserService.GetByEmail(registerModel.UserEmail);

                if (registeredUser == null)
                {
                    registeredUser = this.ServiceManager.UserService.Register(registerModel.UserEmail, registerModel.Password, registerModel.PasswordHint, registerModel.FirstName, registerModel.LastName);
                }
                else
                {
                    TokenModel model = new TokenModel() { Token = registerModel.OAuthToken };
                    return View("Signin", model);
                }
            }

            if (registeredUser != null)
            {
                this.SetCurrentUser(registeredUser);
                TokenModel model = new TokenModel() { Token = registerModel.OAuthToken };
                return View(model);
            }
            else
            {
                return this.View("SignUp", new { oauthToken = registerModel.OAuthToken });
            }
        }

        /// <summary>
        /// Logs a user into the system and authorizes a request token
        /// </summary>
        /// <param name="userName">The username to log in</param>
        /// <param name="password">The users password to sign in</param>
        /// <param name="oauthToken">The oauth token to authorize</param>
        /// <returns>The Grant Access view or the sign in screen again</returns>
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSignin(string userName, string password, string oauthToken)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // yes actually look up the user, but for now
                AMFUserLogin user = this.ServiceManager.UserService.LogonUser(userName, password, this.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString());

                if (user != null)
                {
                    this.SetCurrentUser(user);

                    if (!string.IsNullOrEmpty(oauthToken))
                    {
                        return this.GrantAccess(oauthToken);
                    }
                    else
                    {
                        if (((System.Security.Claims.ClaimsIdentity)User.Identity).HasClaim(ClaimTypes.Role, RoleType.Id.Administrator.ToString()))
                        {
                            return this.Redirect("/Admin/Management/Index");
                        }
                        else 
                        {
                            return this.View(user);
                        }
                    }
                }               
            }

            return this.Signin(oauthToken);
        }

        /// <summary>
        /// Prompt the user to request or deny access to the oauth token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        /// <returns>The view that gives the user the option to grant or deny access</returns>
        [Authorize]
        public ActionResult GrantAccess(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View("GrantAccess", model);              
        }

        private void EliminateUserCookie()
        {
            try
            {
                HttpContext.Authentication.SignOutAsync("AMFCookieMiddlewareInstance");
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }
        }

        [Authorize]
        public ActionResult Signout()
        {
            this.EliminateUserCookie();
            return this.Signin(string.Empty);
        }

        /// <summary>
        /// Approves access for the request token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        [Authorize]
        public void ApproveAccess(string oauthToken)
        {
            try
            {
                ClaimsPrincipal principal = ClaimsPrincipal.Current;

                AMFUserLogin currentUser = null;

                foreach (Claim claim in principal.Claims)
                {
                    if(claim.Type=="UserId")
                    {
                        currentUser = this.ServiceManager.UserService.GetUserById(int.Parse(claim.Value));
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }
        }

        /// <summary>
        /// Denies access to the request token
        /// </summary>
        /// <param name="oauthToken">the request token</param>
        [Authorize]
        public void DenyAccess(string oauthToken)
        {
//            this.RedirectToClient(this.ServiceManager.TokenService.DenyRequestToken(oauthToken), false);
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <param name="emailAddress">The email address of the user</param>
        /// <returns>An MVC view</returns>
        public ActionResult PasswordHint(string emailAddress)
        {
            PasswordHintModel retVal = new PasswordHintModel();

            AMFUserLogin targetUser = this.ServiceManager.UserService.GetByEmail(emailAddress);

            if (targetUser != null)
            {
                retVal.PasswordHint = targetUser.PasswordHint;
            }

            return this.View(retVal);
        }

        public ActionResult ForgotPassword(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View(model);
        }

        public ActionResult ResetPassword(string oauthToken, string userEmail, string resetToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            this.ServiceManager.UserService.ResetPassword(userEmail, EmailConfiguration.GetInstance());
            return this.View("Signin", new { oauth_token = oauthToken });
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        public ActionResult Edit(RegisterModel registerModel)
        {
            AMFUserLogin registeredUser = null;

            if (registerModel != null)
            {
                registeredUser = this.ServiceManager.UserService.Update(registerModel.Id, registerModel.FirstName, registerModel.LastName, registerModel.Password);

                if (registeredUser != null)
                {
                    this.SetCurrentUser(registeredUser);
                }

                return this.View("ProcessSignin", registeredUser);
            }
            else
            {
                return this.View("Signin");
            }
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <returns>An MVC view</returns>
        [Authorize]
        public JsonResult GetByEmail(string emailAddress)
        {
            User retVal = new User();

            if (this.CurrentUser != null)
            {
                retVal = this.ServiceManager.UserService.GetByEmail(emailAddress);
            }

            return Json(retVal);
        }
    }
}
