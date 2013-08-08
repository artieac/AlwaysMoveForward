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

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    /// <summary>
    /// This class contains all the code to extract SiteInfo data from the repository using LINQ
    /// The SiteOnfo object is used for web site specific settings rather than blog specific settings.
    /// </summary>
    /// <param name="dataContext"></param>
    public class SiteInfoRepository : ActiveRecordRepository<SiteInfo, SiteInfoDTO, ISiteInfo>, ISiteInfoRepository
    {
        internal SiteInfoRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

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
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<SiteInfoDTO>.FindFirst());
        }
    }
}
