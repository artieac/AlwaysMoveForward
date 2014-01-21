using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Models;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Controllers;

namespace AlwaysMoveForward.PointChart.Web.Areas.API.Controllers
{
    public class ChartAPIController : BaseController
    {
        //
        // GET: /API/ChartAPI/
        [RequestAuthorizationAttribute]
        public JsonResult GetCharts()
        {
            IList<Chart> userCharts = this.Services.Charts.GetByUser(this.CurrentPrincipal.CurrentUser);
            return Json(userCharts, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /API/ChartAPI/
        [RequestAuthorizationAttribute]
        public JsonResult GetByPointEarnerId(int id)
        {
            PointEarnerModel retVal = new PointEarnerModel();
            retVal.PointEarner = this.Services.PointEarner.GetById(id);
            retVal.Charts = retVal.PointEarner.Charts;
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }
    }
}
