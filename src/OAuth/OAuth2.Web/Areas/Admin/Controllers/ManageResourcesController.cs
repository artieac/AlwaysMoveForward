using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using Microsoft.Extensions.Logging;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityServer4.Models;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    [Authorize(Roles = RoleType.Names.Administrator)]
    public class ManageResourcesController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManageResourcesController(ServiceManagerBuilder serviceManagerBuilder,
                                        IResourceStore resourceStore)
                                        : base(serviceManagerBuilder)
        {
            this.ResourceStore = resourceStore;
        }

        IResourceStore ResourceStore { get; set; }

        public async Task<IActionResult> Index()
        {
            Resources allResources = await this.ResourceStore.GetAllResourcesAsync();
            IList<ApiResource> apiResources = allResources.ApiResources.ToList();
            return this.View(apiResources);
        }
        
        public async Task<IActionResult> Edit(string resourceName)
        {
            ApiResource retVal = null;

            if(String.IsNullOrEmpty(resourceName))
            {
                retVal = new ApiResource();
            }
            else
            {
                retVal = await this.ResourceStore.FindApiResourceAsync(resourceName);
            }

            return this.View(retVal);
        }

        public IActionResult Save(string name, string displayName, bool enabled)
        {
            return this.RedirectToAction("Edit", new { resourceName = name });
        }
    }
}