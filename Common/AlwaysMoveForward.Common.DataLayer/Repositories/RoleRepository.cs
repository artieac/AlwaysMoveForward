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
    /// This class contains all the code to extract Role data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class RoleRepository : ActiveRecordRepositoryA<Role, RoleDTO>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public override string IdPropertyName
        {
            get { return "RoleId"; }
        }

        public override RoleDTO Map(Role source)
        {
            RoleDTO retVal = new RoleDTO();
            retVal.RoleId = source.RoleId;
            retVal.Name = source.Name;
            return retVal;
        }

        public override Role Map(RoleDTO source)
        {
            Role retVal = new Role();
            retVal.RoleId = source.RoleId;
            retVal.Name = source.Name;
            return retVal;
        }

        public override Role Save(Role itemToSave)
        {
            Role retVal = null;

            DetachedCriteria criteria = DetachedCriteria.For<RoleDTO>();
            criteria.Add(Expression.Eq(this.IdPropertyName, itemToSave.RoleId));
            RoleDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<RoleDTO>.FindFirst(criteria);

            if (dtoItem != null)
            {
                dtoItem.Name = itemToSave.Name;
            }
            else
            {
                dtoItem = this.Map(itemToSave);
            }

            this.Save(dtoItem);

            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }
    }
}
