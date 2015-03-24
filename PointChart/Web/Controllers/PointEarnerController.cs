using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Models;
using AlwaysMoveForward.PointChart.Web.Code.Filters;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class PointEarnerController : BaseController
    {
        // GET: /PointEarner/
        [RequestAuthorizationAttribute]
        public ActionResult Index()
        {
            return this.View();
        }

        [RequestAuthorizationAttribute]
        public ActionResult Charts(int id)
        {
            PointEarnerModel model = new PointEarnerModel();
            model.Charts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);
            return this.View(model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult PointsDetail(int id)
        {
            PointEarnerModel model = new PointEarnerModel();
            model.Charts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);
            return this.View(model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult SpendPoints(int pointEarnerId, DateTime dateSpent, double pointsToSpend, string description)
        {
            PointEarnerModel model = new PointEarnerModel();
            model.Charts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);
            return this.View("PointsDetail", model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult DeletePointsSpent(int pointEarnerId, int id)
        {
            PointEarnerModel model = new PointEarnerModel();
            model.Charts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);
            return this.View("PointsDetail", model);
        }      
    }
}
