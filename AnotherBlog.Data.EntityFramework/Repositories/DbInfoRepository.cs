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

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.EntityFramework;

namespace AnotherBlog.Data.EntityFramework.Repositories
{
    public class DbInfoRepository : EntityFrameworkRepository<DbInfo, IDbInfo>, IDbInfoRepository
    {
        internal DbInfoRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public DbInfo GetDbInfo()
        {
            DbInfo retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.DbInfoDTOs select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
    }
}
