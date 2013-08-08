/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Core.Service
{
    public class SiteInfoService : ServiceBase
    {
        internal SiteInfoService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }

        public SiteInfo Create()
        {
            SiteInfo retVal = new SiteInfo();
            retVal.SiteId = this.Repositories.SiteInfo.UnsavedId;
            return retVal;
        }

        public SiteInfo GetSiteInfo()
        {
            return Repositories.SiteInfo.GetSiteInfo();
        }

        public SiteInfo Save(string siteName, string siteUrl, string siteAbout, string siteContact, string defaultTheme, string siteAnalyticsId)
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

            return Repositories.SiteInfo.Save(newItem);
        }
    }
}
