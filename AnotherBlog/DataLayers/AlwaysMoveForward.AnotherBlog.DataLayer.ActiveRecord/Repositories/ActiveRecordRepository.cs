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

using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AutoMapper;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public abstract class ActiveRecordRepository<DomainType, DTOType> : RepositoryBase<DomainType, DTOType>, IRepository<DomainType> 
        where DomainType : class, new()
        where DTOType : class, new()
    {
        public ActiveRecordRepository(IUnitOfWork _unitOfWork) :
            base(_unitOfWork, null)
        {
        }

        public abstract DataMapper.DataMapBase<DomainType, DTOType> DataMapper { get; }

        private DTOType GetDtoById(DomainType domainEntity)
        {
            Object idValue = typeof(DomainType).GetProperty(this.IdPropertyName).GetValue(domainEntity, null);

            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(this.IdPropertyName, idValue));
            return Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindOne(criteria);
        }

        public override DomainType Create()
        {
            return new DomainType();
        }

        public override DomainType GetByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindOne(criteria));
        }

        public override DomainType GetByProperty(string idPropertyName, object idValue, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindOne(criteria));
        }

        public override IList<DomainType> GetAll()
        {
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll());
        }

        public override IList<DomainType> GetAll(int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria));
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria));
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<DTOType>.FindAll(criteria));
        }

        public override DomainType Save(DomainType itemToSave)
        {
            DTOType dtoItem = this.GetDtoById(itemToSave);

            if (dtoItem == null)
            {
                dtoItem = new DTOType();
            }

            if (dtoItem != null)
            {
                dtoItem = this.DataMapper.MapProperties(itemToSave, dtoItem);
                dtoItem = this.Save(dtoItem);
            }

            return this.DataMapper.Map(dtoItem);
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
            bool retVal = true;

            DTOType dtoItem = this.GetDtoById(itemToDelete);

            if (dtoItem != null)
            {
                retVal = this.Delete(dtoItem);
            }

            return retVal;
        }
    }
}
