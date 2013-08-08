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
    public class UnitOfWork : IUnitOfWork, IDisposable
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
            get { return this.currentSession; }
        }

        #region IUnitOfWork Members

        public void StartSession()
        {
            this.currentSession = this.SessionFactory.OpenSession(); ;
        }

        public void EndSession()
        {
            if (this.currentSession!=null)
            {
                this.currentSession.Dispose();
            }
        }

        public IDisposable BeginTransaction()
        {
            return this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public IDisposable BeginTransaction(IsolationLevel isolationLevel)
        {
            IDisposable retVal = null;

            if (this.currentSession == null)
            {
                this.StartSession();
            }

            if (this.currentSession != null)
            {
                retVal = this.currentSession.BeginTransaction();
            }

            return retVal;
        }

        public void EndTransaction(bool canCommit)
        {
            if(this.currentSession!=null)
            {
                if(this.currentSession.Transaction!=null)
                {
                    if (this.currentSession.Transaction.IsActive)
                    {
                        if (canCommit)
                        {
                            this.currentSession.Transaction.Commit();
                        }
                        else
                        {
                            this.currentSession.Transaction.Rollback();
                        }
                    }

                    this.currentSession.Transaction.Dispose();
                }
            }
        }
        
        public void Flush()
        {
            if (this.currentSession != null)
            {
                this.currentSession.Flush();
            }
        }


        #endregion

        public void Dispose()
        {
            this.EndTransaction(true);
            this.EndSession();
        }
    }
}
