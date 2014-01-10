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

using NHibernate.Criterion;
using NHibernate.Transform;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class DbInfoRepository : ActiveRecordRepository<DbInfo, DbInfoDTO>, IDbInfoRepository
    {
        public DbInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, null)
        {

        }

        public override DbInfoDTO Map(DbInfo source)
        {
            DbInfoDTO retVal = new DbInfoDTO();
            retVal.Version = source.Version;
            return retVal;
        }

        public override DbInfo Map(DbInfoDTO source)
        {
            DbInfo retVal = new DbInfo();
            retVal.Version = source.Version;
            return retVal;
        }

        public DbInfo GetDbInfo()
        {
            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<DbInfoDTO>.FindOne());
        }

        public override DbInfo Save(DbInfo itemToSave)
        {
            DbInfo retVal = null;
            DbInfoDTO dtoItem = this.Map(itemToSave);

            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }

        public override bool Delete(DbInfo itemToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
