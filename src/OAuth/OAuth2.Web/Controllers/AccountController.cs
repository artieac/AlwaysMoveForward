using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.Common.Configuration;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.Security;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.Web.Models;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using IdentityServer4.Services;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class AccountController : AMFControllerBase
    {
        private readonly IIdentityServerInteractionService _idsInteractionService;

        public AccountController(ServiceManagerBuilder serviceManagerBuilder,
                                IIdentityServerInteractionService interaction) : base(serviceManagerBuilder)
        {
            this._idsInteractionService = interaction;
        }

        public ActionResult EmbeddedSignin()
        {
            return this.View();
        }

        /// <summary>
        /// Show the initial sign in page
        /// </summary>
        /// <param name="oauth_Token">The oauth token that this sign in is trying to authorize.</param>
        /// <returns>The index view</returns>
        [AllowAnonymous]
        public ActionResult Login(string oauth_Token)
        {
            TokenModel model = new TokenModel() { Token = oauth_Token };

            Consumer consumer = this.ServiceManager.ConsumerService.GetByRequestToken(oauth_Token);

            if (consumer != null)
            {
                model.ConsumerName = consumer.Name;
            }

            return this.View("Login", model);
        }

        /// <summary>
        /// Returns the Signup view for registration.
        /// </summary>
        /// <returns>The MVC View for a registaration</returns>
        [AllowAnonymous]
        public ActionResult SignUp(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View(model);
        }

        /// <summary>
        /// Setup the logged in user on the current thread and setup an auth cookie.
        /// </summary>
        /// <param name="user"></param>
        private void SetCurrentUser(AMFUserLogin user)
        {
//            this.CurrentPrincipal = new OAuthServerSecurityPrincipal(user);
//            FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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
                    return this.View("Signin", model);
                }
            }

            if (registeredUser != null)
            {
                this.SetCurrentUser(registeredUser);
                TokenModel model = new TokenModel() { Token = registerModel.OAuthToken };
                return this.View(model);
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
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSignin(string userName, string password, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AMFUserLogin targetUser = this.ServiceManager.UserService.LogonUser(userName, password, this.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString());

                if (targetUser != null)
                {
                    AuthenticationProperties props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(DefaultUserOptions.RememberMeLoginDuration)
                    };               

                    HttpContext.Authentication.SignInAsync("Password", targetUser.Email, props);

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    if(targetUser.IsInRole(RoleType.Names.Administrator))
                    {
                        return this.Redirect("/Admin/Management/Index");
                    }
                    else
                    {
                        if (_idsInteractionService.IsValidReturnUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                    }

                    return Redirect("~/");

                }

                ModelState.AddModelError("", DefaultUserOptions.InvalidCredentialsErrorMessage);
            }

            // If we got this far, something failed, redisplay form
            return this.Login(returnUrl);
        }

        private void EliminateUserCookie()
        {
            try
            {
//                string cookieName = FormsAuthentication.FormsCookieName;
//                HttpCookie authCookie = this.Response.Cookies[cookieName];

//                if (authCookie != null)
//                {
//                    authCookie.Expires = DateTime.Now.AddDays(-1);
//                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signout()
        {
            this.EliminateUserCookie();
//            this.CurrentPrincipal = new OAuthServerSecurityPrincipal(null);
            return this.Login(string.Empty);
        }

        /// <summary>
        /// Redirect the caller to the oauth call back once the user has been approved/denied
        /// </summary>
        /// <param name="requestToken">The oauth token</param>
        /// <param name="granted">Whether or not access ahs been granted</param>
//        void RedirectToClient(RequestToken requestToken, bool granted)
//        {
//            string consumerKey = string.Empty;
//            string callBackUrl = string.Empty;
//            string verifier = string.Empty;
//            string oauthToken = string.Empty;
//
//            if (requestToken != null)
//            {
//                consumerKey = requestToken.ConsumerKey;
//                oauthToken = requestToken.Token;
//
//                if (requestToken.IsAuthorized() == true)
//                {
//                    verifier = requestToken.VerifierCode;
//                    callBackUrl = requestToken.GenerateCallBackUrl();
//                }
//            }

//            if (!string.IsNullOrEmpty(callBackUrl))
//            {
//                this.Response.Redirect(callBackUrl, true);
//            }
//            else
//            {
//                if (granted)
//                {
//                    string successMessage = string.Format(
//                      "You have been successfully granted Access, To complete the process, please provide <I>{0}</I> with this verification code: <B>{1}</B>",
//                      consumerKey, HttpUtility.HtmlEncode(verifier));

//                    this.Response.Write(successMessage);
//                }
//                else
//                {
//                    this.Response.Write("Denied");
//                }

//                this.Response.End();
//            }
//        }

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
            return this.View("Login", new { oauth_token = oauthToken });
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
                return this.View("Login");
            }
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <returns>An MVC view</returns>
 //       public JsonResult GetByEmail(string emailAddress)
 //       {
 //           User retVal = new User();

//            if (this.CurrentPrincipal.User != null)
//            {
//                retVal = this.ServiceManager.UserService.GetByEmail(emailAddress);
//            }

//            return this.Json(retVal, JsonRequestBehavior.AllowGet);
 //       }
    }
}
