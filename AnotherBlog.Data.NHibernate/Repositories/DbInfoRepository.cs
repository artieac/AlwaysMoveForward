﻿/**
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

using AnotherBlog.Common.Data;
using CE=AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class DbInfoRepository : NHRepository<CE.DbInfo, CE.DbInfo>, IDbInfoRepository
    {
        internal DbInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public CE.DbInfo GetDbInfo()
        {
            return this.FindOne();
        }
    }
}
