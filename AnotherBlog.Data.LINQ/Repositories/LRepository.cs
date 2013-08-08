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
using System.Data.Linq;
using System.Linq.Expressions;

using log4net;
using log4net.Config;

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using LE = AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class LRepository<DomainClass, DTOClass> : IRepository<DomainClass> where DomainClass : class where DTOClass : class, DomainClass
    {
        private ILog logger;
        private IUnitOfWork unitOfWork;

        public LRepository(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

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

        public IUnitOfWork UnitOfWork{ get; set;}

        public virtual DomainClass CreateNewInstance()
        {
            return Activator.CreateInstance<DTOClass>() as DomainClass;
        }

        public virtual string IdPropertyName
        {
            get{ return "Id";}
        }

        public virtual string BlogIdPropertyName
        {
            get { return "BlogId"; }
        }

        protected DTOClass GetDtoById(object idValue)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DTOClass), "dtoParam");

            Expression<Func<DTOClass, bool>> whereExpression = Expression.Lambda<Func<DTOClass, bool>>
            (
                Expression.Equal
                (
                    Expression.Property
                    (
                            dtoParameter,
                            this.IdPropertyName
                    ),
                    Expression.Constant(idValue)
                ),
                new[] { dtoParameter }
            );

            return ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression).Single();
        }

        public DomainClass GetById(int itemId)
        {
            return this.GetByProperty(this.IdPropertyName, itemId);
        }

        public DomainClass GetById(int itemId, CE.Blog targetBlog)
        {
            return this.GetByProperty(this.IdPropertyName, itemId, targetBlog);
        }

        public IList<DomainClass> GetAll()
        {
            IQueryable<DTOClass> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>() select foundItem;
            return dtoList.Cast<DomainClass>().ToList();
        }

        public IList<DomainClass> GetAll(CE.Blog targetBlog)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DTOClass), "dtoParam");

            Expression<Func<DTOClass, bool>> whereExpression = Expression.Lambda<Func<DTOClass, bool>>
            (
                Expression.Equal
                (
                    Expression.Property
                    (
                            dtoParameter,
                            this.BlogIdPropertyName
                    ),
                    Expression.Constant(targetBlog.BlogId)
                ),
                new[] { dtoParameter }
            );

            IQueryable<DTOClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression);
            return dtoList.Cast<DomainClass>().ToList();
        }

        public IList<DomainClass> GetAllByProperty(string propertyName, object idValue)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DTOClass), "dtoParam");

            Expression<Func<DTOClass, bool>> whereExpression = Expression.Lambda<Func<DTOClass, bool>>
            (
                Expression.Equal
                (
                    Expression.Property
                    (
                            dtoParameter,
                            propertyName
                    ),
                    Expression.Constant(idValue)
                ),
                new[] { dtoParameter }
            );

            IQueryable<DTOClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression);
            return dtoList.Cast<DomainClass>().ToList();
        }

        public IList<DomainClass> GetAllByProperty(string propertyName, object idValue, CE.Blog targetBlog)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DTOClass), "dtoParam");

            Expression<Func<DTOClass, bool>> whereExpression = Expression.Lambda<Func<DTOClass, bool>>
            (
                Expression.And
                (
                    Expression.Equal
                    (
                        Expression.Property
                        (
                            dtoParameter,
                            propertyName
                        ),
                        Expression.Constant(idValue)
                    ),
                    Expression.Equal
                    (
                        Expression.Property
                        (
                            dtoParameter,
                            this.BlogIdPropertyName
                        ),
                        Expression.Constant(targetBlog.BlogId)
                    )
                ),
                new[] { dtoParameter }
            );

            IQueryable<DTOClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression);
            return dtoList.Cast<DomainClass>().ToList();
        }

        public DomainClass GetByProperty(string propertyName, object idValue)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DTOClass), "dtoParam");

            Expression<Func<DTOClass, bool>> whereExpression = Expression.Lambda<Func<DTOClass, bool>>
            (
                Expression.Equal
                (
                    Expression.Property
                    (
                            dtoParameter,
                            propertyName
                    ),
                    Expression.Constant(idValue)
                ),
                new[] { dtoParameter }
            );

            DTOClass retVal = null;

            try
            {
                retVal = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression).Single();
            }
            catch (Exception e)
            {

            }

            return retVal;
        }

        public DomainClass GetByProperty(string propertyName, object idValue, CE.Blog targetBlog)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DTOClass), "dtoParam");

            Expression<Func<DTOClass, bool>> whereExpression = Expression.Lambda<Func<DTOClass, bool>>
            (
                Expression.And
                (
                    Expression.Equal
                    (
                        Expression.Property
                        (
                            dtoParameter,
                            propertyName
                        ),
                        Expression.Constant(idValue)
                    ),
                    Expression.Equal
                    (
                        Expression.Property
                        (
                            dtoParameter,
                            this.BlogIdPropertyName
                        ),
                        Expression.Constant(targetBlog.BlogId)
                    )
                ),
                new[] { dtoParameter }
            );

            DTOClass dtoItem = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression).Single();
            return dtoItem;
        }

        public DomainClass Save(DomainClass itemToSave)
        {
            DTOClass targetItem = itemToSave as DTOClass;

            if (targetItem == null)
            {
                ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().InsertOnSubmit(targetItem);
                this.UnitOfWork.Commit();
            }

            return itemToSave;
        }

        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public bool Delete(DomainClass itemToDelete)
        {
            bool retVal = false;

            DTOClass targetItem = itemToDelete as DTOClass;

            if (targetItem != null)
            {
                ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().DeleteOnSubmit(targetItem);
                retVal = true;
                this.UnitOfWork.Commit();
            }

            return retVal;
        }
    }
}
