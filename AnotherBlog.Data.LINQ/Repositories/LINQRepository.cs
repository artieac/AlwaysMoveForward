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
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class LINQRepository<DomainClass, DTOClass, CommonInterface> : RepositoryBase<DomainClass, DTOClass> where DomainClass : class, CommonInterface where DTOClass : class, CommonInterface
    {
        public LINQRepository(IUnitOfWork _unitOfWork, IRepositoryManager repositoryManager) : 
            base(_unitOfWork, repositoryManager)
        {

        }

        public virtual DataMapper<DomainClass, DTOClass, CommonInterface> DataMapper
        {
            get { return DataMapper<DomainClass, DTOClass, CommonInterface>.GetInstance(); }
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

        public override DomainClass GetByProperty(string propertyName, object idValue)
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

            return this.DataMapper.Map(retVal);
        }

        public override DomainClass GetByProperty(string propertyName, object idValue, int blogId)
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
                        Expression.Constant(blogId)
                    )
                ),
                new[] { dtoParameter }
            );

            DTOClass dtoItem = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression).Single();
            return this.DataMapper.Map(dtoItem);
        }

        public override IList<DomainClass> GetAll()
        {
            IQueryable<DTOClass> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>() select foundItem;
            return this.DataMapper.Map(dtoList.ToList());
        }

        public override IList<DomainClass> GetAll(int blogId)
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
                    Expression.Constant(blogId)
                ),
                new[] { dtoParameter }
            );

            IQueryable<DTOClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression);
            return this.DataMapper.Map(dtoList.ToList());
        }

        public override IList<DomainClass> GetAllByProperty(string propertyName, object idValue)
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
            return this.DataMapper.Map(dtoList.ToList());
        }

        public override IList<DomainClass> GetAllByProperty(string propertyName, object idValue, int blogId)
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
                        Expression.Constant(blogId)
                    )
                ),
                new[] { dtoParameter }
            );

            IQueryable<DTOClass> dtoList = ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().Where(whereExpression);
            return this.DataMapper.Map(dtoList.ToList());
        }

        public override DomainClass Save(DomainClass itemToSave)
        {
            DTOClass targetItem = this.DataMapper.Map(itemToSave);

            if (targetItem == null)
            {
                ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<DTOClass>().InsertOnSubmit(targetItem);
                this.UnitOfWork.Commit();
            }

            return this.DataMapper.Map(targetItem);
        }

        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public override bool Delete(DomainClass itemToDelete)
        {
            bool retVal = false;

            DTOClass targetItem = this.DataMapper.Map(itemToDelete);

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
