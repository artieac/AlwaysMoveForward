using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.WebServer.Code;

namespace AlwaysMoveForward.OAuth.WebServer.Areas.Admin.Controllers
{
    [AdminAuthorizeAttribute(RequiredRoles = "Administrator")]
    public class ManagementController : AlwaysMoveForward.OAuth.WebServer.Controllers.ControllerBase
    {
        // GET: Admin/Management
        [AdminAuthorizeAttribute(RequiredRoles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}