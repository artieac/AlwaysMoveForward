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

using AlwaysMoveForward.Common.Data;
using AlwaysMoveForward.Common.Data.Repositories;

using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer.DTO;

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    public abstract class ActiveRecordRepositoryA<DomainType, DTOType> : RepositoryBase<DomainType, DTOType>, IRepository<DomainType>
        where DomainType : class
        where DTOType : class
    {
        public ActiveRecordRepositoryA(IUnitOfWork _unitOfWork, IRepositoryManager repositoryManager) :
            base(_unitOfWork, repositoryManager)
        {
        }

        public abstract DTOType Map(DomainType source);
        public abstract DomainType Map(DTOType source);

        public IList<DomainType> Map(IList<DTOType> source)
        {
            IList<DomainType> retVal = new List<DomainType>();

            if(source!=null)
            {
                for(int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }

        public override DomainType GetByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindOne(criteria));
        }

        public override DomainType GetByProperty(string idPropertyName, object idValue, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            DTOType dtoItem = Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindOne(criteria);
            return this.Map(dtoItem);
        }

        public override IList<DomainType> GetAll()
        {
            IList<DTOType> dtoItems = Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll();
            return this.Map(dtoItems);
        }

        public override IList<DomainType> GetAll(int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            IList<DTOType> dtoItems = Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria);
            return this.Map(dtoItems);
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            IList<DTOType> dtoItems = Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria);
            return this.Map(dtoItems);
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            IList<DTOType> dtoItems = Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria);
            return this.Map(dtoItems);
        }

        public DTOType Save(DTOType itemToSave)
        {
            if (itemToSave != null)
            {
                Castle.ActiveRecord.ActiveRecordMediator<DTOType>.Save(itemToSave);
            }

            return itemToSave;
        }

        public bool Delete(DTOType itemToDelete)
        {
            bool retVal = false;

            if (itemToDelete != null)
            {
                Castle.ActiveRecord.ActiveRecordMediator<DTOType>.Delete(itemToDelete);
                retVal = true;
            }

            return retVal;
        }
        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public override bool Delete(DomainType itemToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
