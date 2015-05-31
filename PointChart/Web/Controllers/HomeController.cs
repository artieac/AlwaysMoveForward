using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /Home/
        [CookieAuthenticationParser]
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.OwnedCharts = this.Services.Charts.GetByCreator(this.CurrentPrincipal.CurrentUser);
            model.AssignedCharts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);

            return this.View(model);
        }

        [Route("Chart/{id}"), HttpGet()]
        [MVCAuthorization]
        public ActionResult Get(long id)
        {
            return View("Chart", id);
        }

        [MVCAuthorization]
        public ActionResult PointEarners()
        {
            return this.View();
        }

        [MVCAuthorization]
        public ActionResult CollectPoints(long id)
        {
            DateTime today = DateTime.Now;
            return this.CollectPoints(id, today.Year, today.Month, today.Day);
        }

        [Route("Home/CollectPoints/{id}/{year}/{month}"), HttpGet()]
        [MVCAuthorization]
        public ActionResult CollectPoints(long id, int year, int month)
        {
            DateTime today = DateTime.Parse(month + "/1/" + year);
            return this.CollectPoints(id, today.Year, today.Month, today.Day);
        }

        [Route("Home/CollectPoints/{id}/{year}/{month}/{day}"), HttpGet()]
        [MVCAuthorization]
        public ActionResult CollectPoints(long id, int year, int month, int day)
        {
            Models.UI.CollectPointsModel retVal = new Models.UI.CollectPointsModel();
            Chart targetChart = this.Services.Charts.GetById(id);

            if(targetChart!=null)
            {
                retVal.ChartId = id;
                retVal.PointEarnerId = targetChart.PointEarner.Id;
            }

            retVal.SelectedDate = DateTime.Parse(month + "/" + day + "/" + year);
            return this.View(retVal);
        }

        [Route("Home/SpendPoints/{id}"), HttpGet()]
        [MVCAuthorization]
        public ActionResult SpendPoints(long id)
        {
            return this.View(id);
        }
    }
}
