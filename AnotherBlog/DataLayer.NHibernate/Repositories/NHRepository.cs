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
    public class NHRepository<DomainType, DTOType> : IRepository<DomainType> where DomainType : class where DTOType : class, DomainType
    {
        private ILog logger;

        public NHRepository(IUnitOfWork _unitOfWork)
        {
            this.UnitOfWork = _unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }

        public virtual DomainType CreateNewInstance()
        {
            return (DomainType)Activator.CreateInstance<DTOType>();
        }

        protected DomainType FindOne()
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            return criteria.SetFirstResult(0).UniqueResult<DomainType>();
        }

        public virtual string IdPropertyName
        {
            get{ return "Id";}
        }

        public DomainType GetById(int itemId)
        {
            return this.GetByProperty(this.IdPropertyName, itemId);
        }

        public DomainType GetById(int itemId, CE.Blog targetBlog)
        {
            return this.GetByProperty(this.IdPropertyName, itemId, targetBlog);
        }

        public IList<DomainType> GetAll()
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            return criteria.List<DomainType>();
        }

        public IList<DomainType> GetAll(CE.Blog targetBlog)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq("Blog", targetBlog));

            return criteria.List<DomainType>();
        }

        public IList<DomainType> GetAllByProperty(string idPropertyName, object idValue)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));

            return criteria.List<DomainType>();
        }

        public IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, CE.Blog targetBlog)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.Add(Expression.Eq("Blog", targetBlog));

            return criteria.List<DomainType>();
        }

        public DomainType GetByProperty(string idPropertyName, object idValue)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));

            return criteria.UniqueResult<DomainType>();
        }

        public DomainType GetByProperty(string idPropertyName, object idValue, CE.Blog targetBlog)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            criteria.Add(Expression.Eq("Blog", targetBlog));

            return criteria.UniqueResult<DomainType>();
        }

        public DomainType Save(DomainType itemToSave)
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
        public bool Delete(DomainType itemToDelete)
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
