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
        // GET: /API/ChartAPI/
        [RequestAuthorizationAttribute]
        public JsonResult GetCharts()
        {
            IList<Chart> userCharts = this.Services.Charts.GetByUser(this.CurrentPrincipal.CurrentUser);
            return this.Json(userCharts, JsonRequestBehavior.AllowGet);
        }

        [RequestAuthorizationAttribute]
        public JsonResult GetAll()
        {
            IList<PointEarner> pointEarnerCharts = this.Services.PointEarner.GetAll(this.CurrentPrincipal.CurrentUser);
            return this.Json(pointEarnerCharts, JsonRequestBehavior.AllowGet);
        }

        // GET: /API/ChartAPI/
        [RequestAuthorizationAttribute]
        public JsonResult GetByPointEarnerId(int id)
        {
            PointEarnerModel retVal = new PointEarnerModel();
            retVal.PointEarner = this.Services.PointEarner.GetById(id);
            retVal.Charts = retVal.PointEarner.Charts;
            return this.Json(retVal, JsonRequestBehavior.AllowGet);
        }
    }
}
