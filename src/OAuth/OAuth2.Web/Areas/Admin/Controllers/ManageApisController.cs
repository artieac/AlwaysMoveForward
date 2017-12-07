using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using Microsoft.AspNetCore.Authorization;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    [Authorize(Roles = RoleType.Names.Administrator)]
    public class ManageApisController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManageApisController(ServiceManagerBuilder serviceManagerBuilder)
                                     : base(serviceManagerBuilder) { }

        public IActionResult Index()
        {
            IList<ApiResources> retVal = this.ServiceManager.ApiResourceService.GetAll();
            return View(retVal);
        }

        public IActionResult Edit(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }

        public IActionResult UpdateSecrets(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }

        public IActionResult UpdateClaims(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }

        public IActionResult UpdateScopes(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }
    }
}