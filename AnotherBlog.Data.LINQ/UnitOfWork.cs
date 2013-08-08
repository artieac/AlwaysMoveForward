using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        static AnotherBlog.Common.DatabaseConfiguration dbConfiguration;
        AnotherBlogDbDataContext dataContext;

        static UnitOfWork()
        {
            dbConfiguration = (AnotherBlog.Common.DatabaseConfiguration)System.Configuration.ConfigurationManager.GetSection("AnotherBlog/DatabaseConfiguration");
        }

        System.Data.Common.DbTransaction currentTransaction;

        #region IUnitOfWork Members

        public void StartSession()
        {
        }

        public void EndSession()
        {
        }

        public IDisposable BeginTransaction()
        {
            return this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public IDisposable BeginTransaction(IsolationLevel isolationLevel)
        {
            if (this.currentTransaction == null)
            {
                currentTransaction = this.dataContext.Connection.BeginTransaction(isolationLevel);
            }

            return currentTransaction;
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

        public void Flush()
        {
            if (this.dataContext != null)
            {
                this.dataContext.SubmitChanges();
            }
        }

        public AnotherBlogDbDataContext DataContext
        {
            get
            {
                if (this.dataContext == null)
                {
                    this.dataContext = new AnotherBlogDbDataContext(new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[UnitOfWork.dbConfiguration.ConnectionString].ConnectionString));
                }

                return this.dataContext;
            }
            set { this.dataContext = value; }
        }

        #endregion

        public void Dispose()
        {
            if (this.currentTransaction != null)
            {
                this.currentTransaction.Dispose();
            }
        }
    }
}
