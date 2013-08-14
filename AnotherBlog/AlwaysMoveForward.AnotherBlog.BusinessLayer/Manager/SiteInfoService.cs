using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class SiteInfoService : ServiceBase
    {
        public SiteInfoService(ModelContext modelContext)
            : base(modelContext)
        {

        }

        public SiteInfo Create()
        {
            return new SiteInfo();
        }

        public SiteInfo GetSiteInfo()
        {
            SiteInfoGateway gateway = new SiteInfoGateway(modelContext.DataContext);
            return gateway.GetSiteInfo();
        }

        public void Save(string siteName, string siteUrl, string siteAbout, string siteContact, string defaultTheme, string siteAnalyticsId)
        {
            SiteInfo newItem = this.GetSiteInfo();

            if (newItem == null)
            {
                newItem = this.Create();
            }

            newItem.Name = siteName;
            newItem.Url = siteUrl;
            newItem.About = siteAbout;
            newItem.ContactEmail = siteContact;
            newItem.DefaultTheme = defaultTheme;
            newItem.SiteAnalyticsId = siteAnalyticsId;

            SiteInfoGateway gateway = new SiteInfoGateway(this.ModelContext.DataContext);
            gateway.Save(newItem);
        }
    }
}
