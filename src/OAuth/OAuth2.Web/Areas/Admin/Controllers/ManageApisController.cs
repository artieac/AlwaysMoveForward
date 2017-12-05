using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Controllers
{
    public class ManageApisController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManageApisController(ServiceManagerBuilder serviceManagerBuilder)
                                     : base(serviceManagerBuilder) { }

        public IActionResult Index()
        {
            IList<ApiResources> retVal = this.ServiceManager.ApiResourceService.GetAll();
            return View(retVal);
        }
    }
}