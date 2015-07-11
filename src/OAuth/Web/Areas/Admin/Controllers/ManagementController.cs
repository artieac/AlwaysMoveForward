using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Web.Code.Filters;

namespace AlwaysMoveForward.OAuth.Web.Areas.Admin.Controllers
{
    [MVCAuthorization(Roles = "Administrator")]
    public class ManagementController : AlwaysMoveForward.OAuth.Web.Controllers.ControllerBase
    {
        // GET: Admin/Management
        [MVCAuthorization(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}