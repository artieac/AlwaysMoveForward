using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AlwaysMoveForward.OAuth2.Web.Code;
using Microsoft.Extensions.Logging;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    [Authorize(Roles = RoleType.Names.Administrator)]
    public class ManagementController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManagementController(ServiceManagerBuilder serviceManagerBuilder) 
                                    : base(serviceManagerBuilder) { }

        // GET: Admin/Management
        public ActionResult Index()
        {
            return View();
        }
    }
}