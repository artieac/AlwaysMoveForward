using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

using AnotherBlog.Common.Data;
using NH = NHibernate;
using NHC = NHibernate.Cfg;

namespace AnotherBlog.Data.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private static NHC.Configuration nhibernateConfig;

        static UnitOfWork()
        {
            nhibernateConfig = new NHC.Configuration();
            nhibernateConfig.Configure();
            nhibernateConfig.AddAssembly(Assembly.GetExecutingAssembly());

            NHC.Environment.UseReflectionOptimizer = false;
        }

        private NH.ISessionFactory sessionFactory;
        private NH.ISession currentSession;
        NH.ITransaction currentTransaction;

        private NH.ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = UnitOfWork.nhibernateConfig.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        public NH.ISession CurrentSession
        {
            get
            {
                if (currentSession == null)
                {
                    currentSession = UnitOfWork.nhibernateConfig.BuildSessionFactory().OpenSession();
                }

                return currentSession;
            }
        }

        #region IUnitOfWork Members

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (currentTransaction == null)
            {
                currentTransaction = sessionFactory.OpenSession().BeginTransaction();
            }
        }

        public void EndTransaction(bool canCommit)
        {
            if (currentTransaction != null)
            {
                if (canCommit)
                {
                    currentTransaction.Commit();
                }
                else
                {
                    currentTransaction.Rollback();
                }

                currentTransaction.Dispose();
                currentTransaction = null;
            }
        }

        public void Commit()
        {
            if (currentSession != null)
            {
                currentSession.Flush();
            }
        }


        #endregion
    }
}
