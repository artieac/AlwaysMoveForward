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

using log4net;
using log4net.Config;

using NHibernate;
using NHibernate.Transform;
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
    public class ActiveRecordRepository<DomainType, DTOType, CommonInterface> : RepositoryBase<DomainType, DTOType>
        where DomainType : class, CommonInterface
        where DTOType : class, CommonInterface, DomainType
    {
        public ActiveRecordRepository(IUnitOfWork _unitOfWork, IRepositoryManager repositoryManager) :
            base(_unitOfWork, repositoryManager)
        {
        }
        
        public override DomainType GetByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindOne(criteria);
        }

        public override DomainType GetByProperty(string idPropertyName, object idValue, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindOne(criteria);
        }

        public override IList<DomainType> GetAll()
        {
            return Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll();
        }

        public override IList<DomainType> GetAll(int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria);
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria);
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria);
        }

        public override DomainType Save(DomainType itemToSave)
        {
            if (itemToSave != null)
            {
                Castle.ActiveRecord.ActiveRecordMediator<DTOType>.Save(itemToSave);
            }

            return itemToSave;
        }

        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public override bool Delete(DomainType itemToDelete)
        {
            bool retVal = false;

            if (itemToDelete != null)
            {
                Castle.ActiveRecord.ActiveRecordMediator<DTOType>.Delete(itemToDelete);
                retVal = true;
            }

            return retVal;
        }
    }
}
