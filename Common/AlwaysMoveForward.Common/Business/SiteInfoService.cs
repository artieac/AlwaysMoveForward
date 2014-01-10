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

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class SiteInfoService
    {
        public SiteInfoService(ServiceContext serviceContext)
        {
            this.UnitOfWork = serviceContext.UnitOfWork;
            this.Repositories = serviceContext.RepositoryManager;
        }

        private IUnitOfWork UnitOfWork { get; set; }
        protected IRepositoryManager Repositories { get; private set; }

        public SiteInfo GetSiteInfo()
        {
            return Repositories.SiteInfo.GetSiteInfo();
        }

        public SiteInfo Save(string siteName, string siteAbout, string siteContact, string defaultTheme, string siteAnalyticsId)
        {
            SiteInfo newItem = this.GetSiteInfo();

            if (newItem == null)
            {
                newItem = new SiteInfo();
            }

            newItem.Name = siteName;
            newItem.About = siteAbout;
            newItem.ContactEmail = siteContact;
            newItem.DefaultTheme = defaultTheme;
            newItem.SiteAnalyticsId = siteAnalyticsId;

            return Repositories.SiteInfo.Save(newItem);
        }
    }
}
