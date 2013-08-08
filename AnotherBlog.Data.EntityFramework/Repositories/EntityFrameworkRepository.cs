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
using System.Linq.Expressions;

using log4net;
using log4net.Config;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.EntityFramework;

namespace AnotherBlog.Data.EntityFramework.Repositories
{
    public class EntityFrameworkRepository<DomainClass, CommonInterface> : RepositoryBase<DomainClass, DomainClass> where DomainClass : class, CommonInterface
    {
        public EntityFrameworkRepository(IUnitOfWork _unitOfWork, IRepositoryManager repositoryManager) : 
            base(_unitOfWork, repositoryManager)
        {

        }

        public virtual string BlogIdPropertyName
        {
            get { return "BlogId"; }
        }

        protected DomainClass GetDtoById(object idValue)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DomainClass), "dtoParam");

            Expression<Func<DomainClass, bool>> whereExpression = Expression.Lambda<Func<DomainClass, bool>>
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

            return ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Where(whereExpression).Single();
        }

        public override DomainClass GetByProperty(string propertyName, object idValue)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DomainClass), "dtoParam");

            Expression<Func<DomainClass, bool>> whereExpression = Expression.Lambda<Func<DomainClass, bool>>
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

            DomainClass retVal = null;

            try
            {
                retVal = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Where(whereExpression).Single();
            }
            catch (Exception e)
            {

            }

            return retVal;
        }

        public override DomainClass GetByProperty(string propertyName, object idValue, int blogId)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DomainClass), "dtoParam");

            Expression<Func<DomainClass, bool>> whereExpression = Expression.Lambda<Func<DomainClass, bool>>
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
                            Expression.Property(dtoParameter, "Blog"),
                            this.BlogIdPropertyName
                        ),
                        Expression.Constant(blogId)
                    )
                ),
                new[] { dtoParameter }
            );

            DomainClass dtoItem = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Where(whereExpression).Single();
            return dtoItem;
        }

        public override IList<DomainClass> GetAll()
        {
            IQueryable<DomainClass> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>() select foundItem;
            return dtoList.ToList();
        }

        public override IList<DomainClass> GetAll(int blogId)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DomainClass), "dtoParam");

            Expression<Func<DomainClass, bool>> whereExpression = Expression.Lambda<Func<DomainClass, bool>>
            (
                Expression.Equal
                (
                    Expression.Property
                    (
                        Expression.Property(dtoParameter, "Blog"), 
                        this.BlogIdPropertyName
                    ),
                    Expression.Constant(blogId)
                ),
                new[] { dtoParameter }
            );

            IQueryable<DomainClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Where(whereExpression);
            return dtoList.ToList();
        }

        public override IList<DomainClass> GetAllByProperty(string propertyName, object idValue)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DomainClass), "dtoParam");

            Expression<Func<DomainClass, bool>> whereExpression = Expression.Lambda<Func<DomainClass, bool>>
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

            IQueryable<DomainClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Where(whereExpression);
            return dtoList.ToList();
        }

        public override IList<DomainClass> GetAllByProperty(string propertyName, object idValue, int blogId)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(DomainClass), "dtoParam");

            Expression<Func<DomainClass, bool>> whereExpression = Expression.Lambda<Func<DomainClass, bool>>
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
                        Expression.Constant(blogId)
                    )
                ),
                new[] { dtoParameter }
            );

            IQueryable<DomainClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Where(whereExpression);
            return dtoList.ToList();
        }

        public override DomainClass Save(DomainClass itemToSave)
        {
            if (itemToSave == null)
            {
                ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Add(itemToSave);
                this.UnitOfWork.Commit();
            }

            return itemToSave;
        }

        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public override bool Delete(DomainClass itemToDelete)
        {
            bool retVal = false;

            if (itemToDelete != null)
            {
                ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DomainClass>().Remove(itemToDelete);
                retVal = true;
                this.UnitOfWork.Commit();
            }

            return retVal;
        }
    }
}
