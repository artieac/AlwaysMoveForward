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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    /// <summary>
    /// This class contains all the code to extract Role data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class RoleRepository : ActiveRecordRepositoryBase<Role, RoleDTO, int>, IRoleRepository
    {
        public RoleRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override RoleDTO GetDTOById(Role domainInstance)
        {
            return this.GetDTOById(domainInstance.RoleId);
        }

        protected override RoleDTO GetDTOById(int idSource)
        {
            DetachedCriteria criteria = DetachedCriteria.For<RoleDTO>();
            criteria.Add(Expression.Eq("RoleId", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<RoleDTO>.FindOne(criteria);
        }

        protected override DataMapBase<Role, RoleDTO> GetDataMapper()
        {
            return new RoleDataMap(); 
        }
    }
}
