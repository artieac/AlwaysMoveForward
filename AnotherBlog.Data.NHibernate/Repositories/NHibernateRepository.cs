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

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.NHibernate;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class NHibernateRepository<DomainType, DTOType> : RepositoryBase<DomainType, DTOType> where DomainType : class where DTOType : class, DomainType
    {
        public NHibernateRepository(IUnitOfWork _unitOfWork, IRepositoryManager repositoryManager) :
            base(_unitOfWork, repositoryManager)
        {

        }

        protected DomainType FindOne()
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            return criteria.SetFirstResult(0).UniqueResult<DomainType>();
        }

        public override DomainType GetByProperty(string idPropertyName, object idValue)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));

            return criteria.UniqueResult<DomainType>();
        }

        public override DomainType GetByProperty(string idPropertyName, object idValue, int blogId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return criteria.UniqueResult<DomainType>();
        }

        public override IList<DomainType> GetAll()
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            return criteria.List<DomainType>();
        }

        public override IList<DomainType> GetAll(int blogId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return criteria.List<DomainType>();
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return criteria.List<DomainType>();
        }

        public override IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, int blogId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return criteria.List<DomainType>();
        }

        public override DomainType Save(DomainType itemToSave)
        {
            DTOType saveType = itemToSave as DTOType;

            if (saveType != null)
            {
                try
                {
                    ((UnitOfWork)this.UnitOfWork).CurrentSession.SaveOrUpdate(saveType);
                    this.UnitOfWork.Commit();
                }
                catch (Exception e)
                {
                    this.Logger.Error(e.Message, e);
                }
            }

            return (DomainType)saveType;
        }

        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public override bool Delete(DomainType itemToDelete)
        {
            bool retVal = false;

            DTOType deleteType = itemToDelete as DTOType;

            if (itemToDelete != null)
            {
                try
                {
                    ((UnitOfWork)this.UnitOfWork).CurrentSession.Delete(deleteType);
                    this.UnitOfWork.Commit();
                    retVal = true;
                }
                catch (Exception e)
                {
                    this.Logger.Error(e.Message, e);
                }
            }

            return retVal;
        }
    }
}
