using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.Web.Models.Consent;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    /// <summary>
    /// This controller processes the consent UI
    /// </summary>
    public class ConsentController : AMFControllerBase
    {
        public ConsentController(ServiceManagerBuilder serviceManagerBuilder,
                                IIdentityServerInteractionService interaction,
                                IResourceStore resourceStore) : base(serviceManagerBuilder)
        {
            this.IdentityServerInteractionService = interaction;
            this.ResourceStore = resourceStore;
        }

        public IIdentityServerInteractionService IdentityServerInteractionService { get; private set; }
        public IResourceStore ResourceStore { get; private set; }

        /// <summary>
        /// Shows the consent screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            ConsentViewModel retVal = new ConsentViewModel();
            var request = await this.IdentityServerInteractionService.GetAuthorizationContextAsync(returnUrl);

            if (request != null)
            {
                Consumer client = this.ServiceManager.ConsumerService.GetConsumer(request.ClientId);

                if (client != null)
                {
                    var resources = await this.ResourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);

                    if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
                    {
                        retVal.ReturnUrl = returnUrl;
                        retVal.ClientName = client.Name;
                        retVal.ClientUrl = String.Empty;
                        retVal.ClientLogoUrl = String.Empty;

                        retVal.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, true)).ToArray();
                        retVal.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, true)).ToArray();                       
                    }
                    else
                    {
                        LogManager.GetLogger().Error("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
                    }
                }
                else
                {
                    LogManager.GetLogger().Error("Invalid client id: {0}", request.ClientId);
                }
            }
            else
            {
                LogManager.GetLogger().Error("No consent request matching request: {0}", returnUrl);
            }

            return View(retVal);
        }

        public ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeViewModel
            {
                Name = identity.Name,
                DisplayName = identity.DisplayName,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required,
            };
        }

        public ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
        {
            return new ScopeViewModel
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Emphasize = scope.Emphasize,
                Required = scope.Required,
                Checked = check || scope.Required,
            };
        }

        /// <summary>
        /// Approves access for the request token
        /// </summary>
        /// <param name="redirectUrl">the return url</param>
        public async Task<IActionResult> ApproveAccess(string redirectUrl, IList<string> ScopesConsented)
        {
            try
            {
                //                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                //                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                //                AMFUserLogin currentUser = this.ServiceManager.UserService.GetUserById(int.Parse(ticket.Name));

                //                if (currentUser != null)
                //                {
                //                    this.CurrentPrincipal = new OAuthServerSecurityPrincipal(currentUser);
                //                    this.RedirectToClient(this.ServiceManager.TokenService.CreateVerifierAndAssociateUserInfo(oauthToken, currentUser), true);
                //                }

                // validate return url is still valid

                AuthorizationRequest request = await this.IdentityServerInteractionService.GetAuthorizationContextAsync(redirectUrl);

                if (request != null)
                {
                    ConsentResponse consentResponse = new ConsentResponse
                    {
                        RememberConsent = false,
                        ScopesConsented = ScopesConsented
                    };

                    await this.IdentityServerInteractionService.GrantConsentAsync(request, consentResponse);
                }

                return Redirect(redirectUrl);
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }

            return View("Error");
        }

        /// <summary>
        /// Denies access to the request token
        /// </summary>
        /// <param name="redirectUrl">the return url</param>
        public async Task<IActionResult> DenyAccess(string redirectUrl)
        {
            return View("Error");
        }
    }
}
