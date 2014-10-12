using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// Creats a new Consumer Key/Secret
    /// </summary>
    public class ConsumerController : ControllerBase
    {
        /// <summary>
        /// Accepts the parameters as a get call.
        /// </summary>
        /// <param name="request">The request parameters</param>
        /// <returns>The new consumer key and secret</returns>
        public object Post(PostConsumerRequest request)
        {
            string retVal = string.Empty;

            try
            {
                Consumer newConsumer = this.ServiceManager.ConsumerService.Create(request.Name, request.ContactEmail);

                if (newConsumer != null)
                {
                    retVal = string.Format("consumerKey={0}&consumerSecret={1}", newConsumer.ConsumerKey, newConsumer.ConsumerSecret);
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }

            return retVal; 
        }
    }
}
