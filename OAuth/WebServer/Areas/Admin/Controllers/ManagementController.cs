using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VP.Digital.Common.Entities;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.WebServer.Code;

namespace VP.Digital.Security.OAuth.WebServer.Areas.Admin.Controllers
{
    [AdminAuthorizeAttribute(RequiredRoles = "Administrator")]
    public class ManagementController : VP.Digital.Security.OAuth.WebServer.Controllers.ControllerBase
    {
        // GET: Admin/Management
        [AdminAuthorizeAttribute(RequiredRoles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}