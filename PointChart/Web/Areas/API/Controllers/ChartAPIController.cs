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
        public JsonResult GetChartsByCreator()
        {
            IList<Chart> userCharts = this.Services.Charts.GetByCreator(this.CurrentPrincipal.CurrentUser);
            return this.Json(userCharts, JsonRequestBehavior.AllowGet);
        }

        [RequestAuthorizationAttribute]
        public JsonResult GetChartsByPointEarner()
        {
            IList<Chart> userCharts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);
            return this.Json(userCharts, JsonRequestBehavior.AllowGet);
        }

        [RequestAuthorizationAttribute]
        public JsonResult GetAll()
        {
            IList<Chart> creatorCharts = this.Services.Charts.GetByCreator(this.CurrentPrincipal.CurrentUser);
            IList<Chart> pointEarnerCharts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);
            return this.Json(pointEarnerCharts, JsonRequestBehavior.AllowGet);
        }

        [RequestAuthorizationAttribute]
        public ActionResult CompleteTask(int chartId, int taskId, int numberOfTimesCompleted, DateTime dateCompleted)
        {
            this.Services.Charts.AddCompletedTask(chartId, taskId, dateCompleted, numberOfTimesCompleted, this.CurrentPrincipal.CurrentUser);
            return this.Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
