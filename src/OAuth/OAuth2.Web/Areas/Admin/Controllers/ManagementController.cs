using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Controllers
{
//    [MVCAuthorization(Roles = "Administrator")]
    public class ManagementController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManagementController(ServiceManagerBuilder serviceManagerBuilder) : base(serviceManagerBuilder) { }

        // GET: Admin/Management
        //        [MVCAuthorization(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}