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
    [Authorize(Roles = RoleType.Names.Administrator)]
    public class ManageApisController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManageApisController(ServiceManagerBuilder serviceManagerBuilder)
                                     : base(serviceManagerBuilder) { }

        [Route("admin/ManageApis/Index")]
        public IActionResult Index()
        {
            IList<ApiResources> retVal = this.ServiceManager.ApiResourceService.GetAll();
            return View(retVal);
        }

        [Route("admin/ManageApis/Edit/{id}")]
        public IActionResult Edit(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }

        [Route("admin/ManageApis/Secrets/{id}")]
        public IActionResult Secrets(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }

        [Route("admin/ManageApis/Claims/{id}")]
        public IActionResult Claims(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }

        [Route("admin/ManageApis/Scopes/{id}")]
        public IActionResult Scopes(long id)
        {
            ApiResources retVal = this.ServiceManager.ApiResourceService.GetById(id);
            return this.View(retVal);
        }
    }
}