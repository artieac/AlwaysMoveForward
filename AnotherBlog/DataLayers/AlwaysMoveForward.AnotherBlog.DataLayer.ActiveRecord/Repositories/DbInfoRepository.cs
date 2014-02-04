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
using NHibernate.Transform;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class DbInfoRepository : ActiveRecordRepository<DbInfo, DbInfoDTO>, IDbInfoRepository
    {
        public DbInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override DataMapBase<DbInfo, DbInfoDTO> DataMapper
        {
            get { return DataMapManager.Mappers().DbInfoMapper; }
        }

        public DbInfo GetDbInfo()
        {
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<DbInfoDTO>.FindOne());
        }

        public override bool Delete(DbInfo itemToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
