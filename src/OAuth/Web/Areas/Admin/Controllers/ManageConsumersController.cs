﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Web.Code.Filters;
using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.OAuth.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// This class provides the UI elemetns to manage the consumers 
    /// </summary>
    [MVCAuthorization(Roles = "Administrator")]
    public class ManageConsumersController : AlwaysMoveForward.OAuth.Web.Controllers.ControllerBase
    {
        /// <summary>
        /// Lists all the consumers
        /// </summary>
        /// <returns>A view</returns>
        public ActionResult Index(int? page)
        {
            int pageIndex = 0;

            if (page.HasValue)
            {
                pageIndex = page.Value - 1;
            }

            IPagedList<Consumer> retVal = new PagedList<Consumer>(this.ServiceManager.ConsumerService.GetAll(), pageIndex, AlwaysMoveForward.OAuth.Web.Models.PagedListModel<int>.PageSize);
            return this.View(retVal);
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