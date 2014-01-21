using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Controllers;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models;

namespace AlwaysMoveForward.PointChart.Web.Areas.API.Controllers
{
    public class PointEarnerAPIController : BaseController
    {
        [RequestAuthorizationAttribute]
        public JsonResult GetAll()
        {
            IList<PointEarner> retVal = this.Services.PointEarner.GetAll(this.CurrentPrincipal.CurrentUser);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        [RequestAuthorizationAttribute]
        public ActionResult Delete()
        {
            return View();
        }

        [RequestAuthorizationAttribute]
        public JsonResult Update(String firstName, String lastName, String email)
        {
            this.Services.PointEarner.AddOrUpdate(firstName, lastName, email, this.CurrentPrincipal.CurrentUser);
            IList<PointEarner> retVal = this.Services.PointEarner.GetAll(this.CurrentPrincipal.CurrentUser);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        [RequestAuthorizationAttribute]
        public JsonResult Add(String firstName, String lastName, String email)
        {
            this.Services.PointEarner.AddOrUpdate(firstName, lastName, email, this.CurrentPrincipal.CurrentUser);
            IList<PointEarner> retVal = this.Services.PointEarner.GetAll(this.CurrentPrincipal.CurrentUser);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /API/ChartAPI/
        [RequestAuthorizationAttribute]
        public JsonResult GetChartsByPointEarnerId(int id)
        {
            PointEarnerModel retVal = new PointEarnerModel();
            retVal.PointEarner = this.Services.PointEarner.GetById(id);
            retVal.Charts = retVal.PointEarner.Charts;
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

    }
}
