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
using IdentityServer4;
using AlwaysMoveForward.OAuth2.Web.Models.Account;
using System.Security.Claims;
using AlwaysMoveForward.OAuth2.Web.Code;

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

        /// <summary>
        /// Show the initial sign in page
        /// </summary>
        /// <param name="returnUrl">The url to return to after login.</param>
        /// <returns>The index view</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string consumerName)
        {
            // Clear the existing external cookie to ensure a clean login process
            HttpContext.Authentication.SignOutAsync(SiteConstants.AuthenticationScheme);

            LoginModel retVal = new LoginModel();
            retVal.ReturnUrl = returnUrl;
            retVal.ConsumerName = consumerName;

            return View(retVal);
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
        public ActionResult ProcessLogin(ProcessLoginInput input)
        {
            if (ModelState.IsValid)
            {
                AMFUserLogin targetUser = this.ServiceManager.UserService.LogonUser(input.UserName, input.Password, this.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString());

                if (targetUser != null)
                {
                    AuthenticationProperties props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(DefaultUserOptions.RememberMeLoginDuration)
                    };

                    ClaimsPrincipalFactory claimsPrincipalFactory = new ClaimsPrincipalFactory();
                    //                    HttpContext.Authentication.SignInAsync(SiteConstants.AuthenticationScheme, claimsPrincipalFactory.Create(targetUser), props);
                    //                    HttpContext.Authentication.SignInAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme, claimsPrincipalFactory.Create(targetUser), props);
                    //_events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username));
                    HttpContext.Authentication.SignInAsync(targetUser.Id.ToString(), targetUser.Email, props);

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    if (string.IsNullOrEmpty(input.ReturnUrl))
                    {
                        if (targetUser.IsInRole(RoleType.Names.Administrator))
                        {
                            return this.Redirect(SiteConstants.PageLocations.AdminHome);
                        }
                    }
                    else
                    {
                        if (_idsInteractionService.IsValidReturnUrl(input.ReturnUrl))
                        {
                            return Redirect(input.ReturnUrl);
                        }
                    }

                    return Redirect("~/");
                }

                ModelState.AddModelError("", DefaultUserOptions.InvalidCredentialsErrorMessage);
            }

            // If we got this far, something failed, redisplay form
            LoginModel loginModel = new LoginModel() { ReturnUrl = input.ReturnUrl };
            return this.View("Login", loginModel);
        }

        /// <summary>
        /// Returns the Signup view for registration.
        /// </summary>
        /// <returns>The MVC View for a registaration</returns>
        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            RegisterModel retVal = new RegisterModel();
            retVal.ReturnUrl = returnUrl;
            return this.View(retVal);
        }


        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessRegister(ProcessRegisterInput registerModel)
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
                    RegisterModel model = new RegisterModel() { ReturnUrl = registerModel.ReturnUrl, FoundUser = true };
                    return this.View("Register", model);
                }
            }

            if (registeredUser != null)
            {
                LoginModel model = new LoginModel() { ReturnUrl = registerModel.ReturnUrl };
                return this.View("Login", model);
            }
            else
            {
                EditModel model = new EditModel(registeredUser);
                return this.View("Register", model);
            }
        }
  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync(SiteConstants.AuthenticationScheme);
            LoginModel retVal = new LoginModel();
            return this.View("Login", retVal);
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

        public ActionResult ForgotPassword(string returnUrl)
        {
            LoginModel model = new LoginModel() { ReturnUrl = returnUrl };
            return this.View(model);
        }

        public ActionResult ResetPassword(string returnUrl, string userEmail, string resetToken)
        {
            LoginModel model = new LoginModel() { ReturnUrl = returnUrl };
            this.ServiceManager.UserService.ResetPassword(userEmail, EmailConfiguration.GetInstance());
            return this.View("Login", model);
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="editModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        public ActionResult Edit(EditModel editModel)
        {
            AMFUserLogin registeredUser = null;

            if (editModel != null)
            {
                registeredUser = this.ServiceManager.UserService.Update(editModel.Id, editModel.FirstName, editModel.LastName, editModel.Password);

                if(registeredUser != null)
                {
                    return this.View("ProcessSignin", registeredUser);
                }
                else
                {
                    return this.View("Login");
                }
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
