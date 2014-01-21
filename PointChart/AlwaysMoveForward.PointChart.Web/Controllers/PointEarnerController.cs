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
        //
        // GET: /PointEarner/
        [RequestAuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [RequestAuthorizationAttribute]
        public ActionResult Charts(int id)
        {
            PointEarnerModel model = new PointEarnerModel();
            model.PointEarner = this.Services.PointEarner.GetById(id);
            model.Charts = this.Services.Charts.GetByPointEarner(id, this.CurrentPrincipal.CurrentUser);
            return View(model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult PointsDetail(int id)
        {
            PointEarnerModel model = new PointEarnerModel();
            model.PointEarner = this.Services.PointEarner.GetById(id);
            model.Charts = this.Services.Charts.GetByPointEarner(id, this.CurrentPrincipal.CurrentUser);
            return View(model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult SpendPoints(int pointEarnerId, DateTime dateSpent, double pointsToSpend, String description)
        {
            PointEarnerModel model = new PointEarnerModel();
            model.PointEarner = this.Services.PointEarner.SpendPoints(pointEarnerId, pointsToSpend, dateSpent, description);
            model.Charts = this.Services.Charts.GetByPointEarner(pointEarnerId, this.CurrentPrincipal.CurrentUser);
            return View("PointsDetail", model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult DeletePointsSpent(int pointEarnerId, int id)
        {
            PointEarnerModel model = new PointEarnerModel();
            this.Services.PointEarner.DeleteSpentPoints(pointEarnerId, id);
            model.PointEarner = this.Services.PointEarner.GetById(pointEarnerId);
            model.Charts = this.Services.Charts.GetByPointEarner(pointEarnerId, this.CurrentPrincipal.CurrentUser);
            return View("PointsDetail", model);
        }
    }
}
