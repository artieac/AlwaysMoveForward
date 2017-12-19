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
using AlwaysMoveForward.OAuth2.Web.Models.Consent;
using Microsoft.AspNetCore.Authorization;
using AlwaysMoveForward.OAuth2.Web.Code.IdentityServer;
using Microsoft.Extensions.Logging;
using AlwaysMoveForward.Core.Common.Utilities;

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


        private ScopeViewModel GetOfflineAccessScope(bool check)
        {
            return new ScopeViewModel
            {
                Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = ConsentOptions.OfflineAccessDisplayName,
                Description = ConsentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }
        
        /// <summary>
        /// Shows the consent screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Consent/Index")]
        [Authorize]
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

                        retVal.IdentityScopes = resources.IdentityResources.Where(x => request.ScopesRequested.Contains(x.Name))
                            .Select(x => CreateScopeViewModel(x, true));
                        retVal.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes)
                            .Where(x => request.ScopesRequested.Contains(x.Name))
                            .Select(x => CreateScopeViewModel(x, true));

                        if(request.ScopesRequested.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) && ConsentOptions.EnableOfflineAccess)
                        {
                            retVal.ResourceScopes = retVal.ResourceScopes.Union(new ScopeViewModel[]
                            {
                                GetOfflineAccessScope(true)
                            });
                        }
                    }
                    else
                    {
                        LogManager.CreateLogger<ConsentController>().LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
                    }
                }
                else
                {
                    LogManager.CreateLogger<ConsentController>().LogError("Invalid client id: {0}", request.ClientId);
                }
            }
            else
            {
                LogManager.CreateLogger<ConsentController>().LogError("No consent request matching request: {0}", returnUrl);
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

        public ScopeViewModel CreateScopeViewModel(ApiResource targetResource, bool check)
        {
            return new ScopeViewModel
            {
                Name = targetResource.Name,
                DisplayName = targetResource.DisplayName,
                Description = targetResource.Description,
                Checked = check,
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
        [Authorize]
        public async Task<IActionResult> ApproveAccess(string redirectUrl, IList<string> scopesConsented)
        {
            try
            {
                AuthorizationRequest request = await this.IdentityServerInteractionService.GetAuthorizationContextAsync(redirectUrl);

                if (request != null)
                {
                    ConsentResponse consentResponse = new ConsentResponse
                    {
                        RememberConsent = true,
                        ScopesConsented = scopesConsented
                    };

                    await this.IdentityServerInteractionService.GrantConsentAsync(request, consentResponse);
                }

                return Redirect(redirectUrl);
            }
            catch (Exception e)
            {
                LogManager.CreateLogger<ConsentController>().LogError(e, e.Message);
            }

            return View("Error");
        }
    }
}
