using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// This class contains all the code to extract SiteInfo data from the repository using LINQ
    /// The SiteOnfo object is used for web site specific settings rather than blog specific settings.
    /// </summary>
    /// <param name="dataContext"></param>
    public class SiteInfoGateway : GatewayBase
    {
        public SiteInfoGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// Get stored web site settings.
        /// </summary>
        /// <returns></returns>
        public SiteInfo GetSiteInfo()
        {
            SiteInfo retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.SiteInfos select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Update the current site settings.
        /// </summary>
        /// <param name="itemToSave"></param>
        public void Save(SiteInfo itemToSave)
        {
            SiteInfo targetItem = this.GetSiteInfo();

            if (targetItem == null)
            {
                this.DataContext.SiteInfos.InsertOnSubmit(itemToSave);
            }

            this.SubmitChanges();
        }
    }
}
