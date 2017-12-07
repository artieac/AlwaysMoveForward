using AlwaysMoveForward.Core.Common.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AlwaysMoveForward.Core.DataLayer.EntityFramework
{
    public abstract class EntityFrameworkRepositoryBase<TDomainType, TDTOType, TDataContext, TIDType> : RepositoryBase<EFUnitOfWork<TDataContext>, TDomainType, TDTOType, TIDType>
        where TDomainType : class, new()
        where TDTOType : class, new()
        where TDataContext : DbContext
    {
        public EntityFrameworkRepositoryBase(EFUnitOfWork<TDataContext> _unitOfWork) : base(_unitOfWork)
        {
        }

        protected abstract string IdPropertyName { get; }

        protected abstract DbSet<TDTOType> GetEntityInstance();

        public virtual TDTOType GetDTOByDomain(TDomainType domainEntity)
        {
            Object idValue = typeof(TDomainType).GetProperty(this.IdPropertyName).GetValue(domainEntity, null);
            return this.GetDTOByProperty(this.IdPropertyName, idValue);
        }

        public TDTOType GetDTOByProperty(String propertyName, object idValue)
        {
            TDTOType retVal = null;

            ParameterExpression dtoParameter = Expression.Parameter(typeof(TDTOType), "dtoParam");

            Expression<Func<TDTOType, bool>> whereExpression = Expression.Lambda<Func<TDTOType, bool>>
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

            IQueryable<TDTOType> dtoItems = this.GetEntityInstance().Where(whereExpression);

            if (dtoItems != null && dtoItems.Count() > 0)
            {
                retVal = dtoItems.Single();
            }

            return retVal;
        }

        public override TDomainType GetByProperty(string propertyName, object idValue)
        {
            return this.GetDataMapper().Map(this.GetDTOByProperty(propertyName, idValue));
        }

        public override IList<TDomainType> GetAll()
        {
            IQueryable<TDTOType> dtoList = from foundItem in this.GetEntityInstance() select foundItem;
            return this.GetDataMapper().Map(dtoList);
        }

        public override IList<TDomainType> GetAllByProperty(string propertyName, object idValue)
        {
            ParameterExpression dtoParameter = Expression.Parameter(typeof(TDTOType), "dtoParam");

            Expression<Func<TDTOType, bool>> whereExpression = Expression.Lambda<Func<TDTOType, bool>>
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

            IQueryable<TDTOType> dtoList = this.GetEntityInstance().Where(whereExpression);

            return this.GetDataMapper().Map(dtoList);
        }

        public override TDomainType Save(TDomainType itemToSave)
        {
            if (itemToSave != null)
            {
                TDTOType dtoItemToSave = this.GetDTOByDomain(itemToSave);

                if (dtoItemToSave == null)
                {
                    dtoItemToSave = this.GetDataMapper().Map(itemToSave);
                    ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.Add<TDTOType>(dtoItemToSave);
                }
                else
                {
                    dtoItemToSave = this.GetDataMapper().Map(itemToSave, dtoItemToSave);
                    ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.Update<TDTOType>(dtoItemToSave);
                }

                ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.SaveChanges();
            }

            return itemToSave;
        }

        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public override bool Delete(TDomainType itemToDelete)
        {
            bool retVal = false;

            if (itemToDelete != null)
            {
                TDTOType dtoItemToDelete = this.GetDTOByDomain(itemToDelete);

                if (dtoItemToDelete != null)
                {
                    ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.Remove<TDTOType>(dtoItemToDelete);
                    ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.SaveChanges();
                }

                retVal = true;
            }

            return retVal;
        }

        public IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                System.Reflection.PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
