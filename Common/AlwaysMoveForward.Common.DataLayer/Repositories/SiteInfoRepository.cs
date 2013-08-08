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

using AlwaysMoveForward.Common.Data;
using AlwaysMoveForward.Common.Data.Entities;
using AlwaysMoveForward.Common.Data.Repositories;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer.DTO;

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    /// <summary>
    /// This class contains all the code to extract SiteInfo data from the repository using LINQ
    /// The SiteOnfo object is used for web site specific settings rather than blog specific settings.
    /// </summary>
    /// <param name="dataContext"></param>
    public class SiteInfoRepository : ActiveRecordRepositoryA<SiteInfo, SiteInfoDTO>, ISiteInfoRepository
    {
        public SiteInfoRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public override SiteInfoDTO Map(SiteInfo source)
        {
            SiteInfoDTO retVal = new SiteInfoDTO();
            retVal.About = source.About;
            retVal.ContactEmail = source.ContactEmail;
            retVal.DefaultTheme = source.DefaultTheme;
            retVal.Name = source.Name;
            retVal.SiteAnalyticsId = source.SiteAnalyticsId;
            retVal.SiteId = source.SiteId;
            return retVal;
        }

        public override SiteInfo Map(SiteInfoDTO source)
        {
            SiteInfo retVal = new SiteInfo();
            retVal.About = source.About;
            retVal.ContactEmail = source.ContactEmail;
            retVal.DefaultTheme = source.DefaultTheme;
            retVal.Name = source.Name;
            retVal.SiteAnalyticsId = source.SiteAnalyticsId;
            retVal.SiteId = source.SiteId;
            return retVal;
        }

        public override string IdPropertyName
        {
            get { return "SiteId"; }
        }
        /// <summary>
        /// Get stored web site settings.
        /// </summary>
        /// <returns></returns>
        public SiteInfo GetSiteInfo()
        {
            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<SiteInfoDTO>.FindFirst());
        }

        public override SiteInfo Save(SiteInfo source)
        {
            SiteInfo retVal = null;

            SiteInfoDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<SiteInfoDTO>.FindFirst();

            if (dtoItem != null)
            {
                dtoItem.About = source.About;
                dtoItem.ContactEmail = source.ContactEmail;
                dtoItem.DefaultTheme = source.DefaultTheme;
                dtoItem.Name = source.Name;
                dtoItem.SiteAnalyticsId = source.SiteAnalyticsId;
                dtoItem.SiteId = source.SiteId;

                dtoItem = this.Save(dtoItem);

                if (dtoItem != null)
                {
                    retVal = this.Map(dtoItem);
                }
            }

            return retVal;
        }

        public override bool Delete(SiteInfo itemToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
