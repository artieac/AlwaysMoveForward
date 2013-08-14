using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

using NHibernate;
using NHibernate.Cfg;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.AnotherBlog.DataLayer
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        static ISessionFactory sessionFactory;

        static UnitOfWork()
        {
            Castle.ActiveRecord.Framework.IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activeRecord") as Castle.ActiveRecord.Framework.IConfigurationSource;
            Castle.ActiveRecord.ActiveRecordStarter.Initialize(Assembly.GetExecutingAssembly(), source);
            
            NHibernate.Cfg.Environment.UseReflectionOptimizer = false;
        }

        SessionScope sessionScope;
        TransactionScope transactionScope;

        public UnitOfWork()
        {
            this.StartSession();
        }

        #region IUnitOfWork Members

        public void StartSession()
        {
            if (this.sessionScope == null)
            {
                this.sessionScope = new SessionScope();
            }
        }

        public void EndSession()
        {
            if (this.sessionScope != null)
            {
                this.sessionScope.Dispose();
            }
        }

        public IDisposable BeginTransaction()
        {
            return this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public IDisposable BeginTransaction(IsolationLevel isolationLevel)
        {
            transactionScope = new TransactionScope(TransactionMode.Inherits, isolationLevel, OnDispose.Commit);
            return transactionScope;
        }

        public void EndTransaction(bool canCommit)
        {
            if(this.transactionScope!=null)
            {
                if (canCommit == true)
                {
                    this.transactionScope.VoteCommit();
                }
                else
                {
                    this.transactionScope.VoteRollBack();
                }

                this.transactionScope.Flush();
            }
        }

        public void Flush()
        {
            if (this.transactionScope != null)
            {
                this.transactionScope.Flush();
            }
        }

        #endregion

        public void Dispose()
        {
            if (this.transactionScope != null)
            {
                this.transactionScope.Dispose();
                this.transactionScope = null;
            }
        }
    }
}
