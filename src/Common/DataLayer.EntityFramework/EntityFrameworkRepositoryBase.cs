using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AlwaysMoveForward.Core.DataLayer.EntityFramework
{
    public abstract class EntityFrameworkRepositoryBase<DomainClass, DTOClass, TDataContext> 
        where DomainClass : class, new()
        where DTOClass : class, DomainClass, new()
        where TDataContext : DbContext
    {
        public EntityFrameworkRepositoryBase(EFUnitOfWork<TDataContext> _unitOfWork) 
        {
            this.UnitOfWork = UnitOfWork;
        }

        private EFUnitOfWork<TDataContext> UnitOfWork { get; set; }
        public DomainClass Create()
        {
            return new DTOClass();
        }

        protected abstract string IdPropertyName { get; }

        protected abstract DbSet<DTOClass> GetEntityInstance();

        public virtual DTOClass GetDTOByDomain(DomainClass domainEntity)
        {
            Object idValue = typeof(DomainClass).GetProperty(this.IdPropertyName).GetValue(domainEntity, null);
            return this.GetDTOByProperty(this.IdPropertyName, idValue);
        }

        public DTOClass GetDTOByProperty(String propertyName, object idValue)
        {
            DTOClass retVal = null;

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

            IQueryable<DTOClass> dtoItems = this.GetEntityInstance().Where(whereExpression);

            if (dtoItems != null && dtoItems.Count() > 0)
            {
                retVal = dtoItems.Single();
            }

            return retVal;
        }

        public DomainClass GetByProperty(string propertyName, object idValue)
        {
            return this.GetDTOByProperty(propertyName, idValue);
        }

        public IList<DomainClass> GetAll()
        {
            IQueryable<DTOClass> dtoList = from foundItem in this.GetEntityInstance() select foundItem;
            return dtoList.ToList<DomainClass>();
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

            IQueryable<DTOClass> dtoList = this.GetEntityInstance().Where(whereExpression);

            return dtoList.ToList<DomainClass>();
        }

        public DomainClass Save(DomainClass itemToSave)
        {
            if (itemToSave != null)
            {
                DTOClass dtoItemToSave = this.GetDTOByDomain(itemToSave);

                if (dtoItemToSave == null)
                {
                    dtoItemToSave = itemToSave as DTOClass;

                    if (dtoItemToSave != null)
                    {
                        ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.Add<DTOClass>(dtoItemToSave);
                    }
                }

                ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.SaveChanges();
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

            if (itemToDelete != null)
            {
                DTOClass dtoItemToDelete = this.GetDTOByDomain(itemToDelete);

                if (dtoItemToDelete != null)
                {
                    ((EFUnitOfWork<TDataContext>)this.UnitOfWork).DataContext.Remove<DTOClass>(dtoItemToDelete);
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
