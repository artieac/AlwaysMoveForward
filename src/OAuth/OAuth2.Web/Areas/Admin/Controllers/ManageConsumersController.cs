using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using AlwaysMoveForward.Core.Common.Utilities;
using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// This class provides the UI elemetns to manage the consumers 
    /// </summary>
    [Area("admin")]
    [Authorize(Roles = RoleType.Names.Administrator)]
    public class ManageConsumersController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManageConsumersController(ServiceManagerBuilder serviceManagerBuilder)
                                     : base(serviceManagerBuilder) { }

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

            IPagedList<Client> retVal = new PagedList<Client>(this.ServiceManager.ClientService.GetAll(), pageIndex, AlwaysMoveForward.OAuth2.Web.Models.PagedListModel<int>.PageSize);
            return this.View(retVal);
        }

        /// <summary>
        /// Display a consuemr to edit
        /// </summary>
        /// <param name="id">The consumer key</param>
        /// <returns>A view</returns>
        [Route("admin/ManageConsumers/Edit/{id?}")]
        public ActionResult Edit(long? id)
        {
            long retVal = -1;

            if (id.HasValue)
            {
                retVal = id.Value;
            }

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
                    Client newClient = this.ServiceManager.ClientService.Add(consumer.Name, consumer.Name, consumer.Name, true);
                    this.ServiceManager.UnitOfWork.EndTransaction(true);
                }
            }

            return this.RedirectToAction("Index");
        }
    }
}