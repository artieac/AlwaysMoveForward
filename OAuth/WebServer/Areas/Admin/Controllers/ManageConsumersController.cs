using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VP.Digital.Common.Entities;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.WebServer.Code;
using VP.Digital.Common.DataLayer;

namespace VP.Digital.Security.OAuth.WebServer.Areas.Admin.Controllers
{
    /// <summary>
    /// This class provides the UI elemetns to manage the consumers 
    /// </summary>
    [AdminAuthorizeAttribute(RequiredRoles = "Administrator")]
    public class ManageConsumersController : VP.Digital.Security.OAuth.WebServer.Controllers.ControllerBase
    {
        /// <summary>
        /// Lists all the consumers
        /// </summary>
        /// <returns>A view</returns>
        public ActionResult Index()
        {
            IList<Consumer> consumers = this.ServiceManager.ConsumerService.GetAll();
            return this.View(consumers);
        }

        /// <summary>
        /// Display a consuemr to edit
        /// </summary>
        /// <param name="id">The consumer key</param>
        /// <returns>A view</returns>
        public ActionResult Edit(string id)
        {
            Consumer retVal = this.ServiceManager.ConsumerService.GetConsumer(id);
            return this.View(retVal);
        }

        /// <summary>
        /// Saves changes to a consumer
        /// </summary>
        /// <param name="consumer">The consumer</param>
        /// <returns>A view</returns>
        public ActionResult Save(Consumer consumer)
        {
            if(consumer != null)
            {
                using (this.ServiceManager.UnitOfWork.BeginTransaction())
                {
                    this.ServiceManager.ConsumerService.Save(consumer);
                    this.ServiceManager.UnitOfWork.EndTransaction(true);
                }
            }

            return this.RedirectToAction("Index");
        }
    }
}