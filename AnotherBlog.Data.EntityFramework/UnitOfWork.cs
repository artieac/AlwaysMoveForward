using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.EntityFramework.Entities;

namespace AnotherBlog.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        static AnotherBlog.Common.DatabaseConfiguration dbConfiguration;
        AnotherBlogDataContext dataContext;

        static UnitOfWork()
        {
            dbConfiguration = (AnotherBlog.Common.DatabaseConfiguration)System.Configuration.ConfigurationManager.GetSection("AnotherBlog/DatabaseConfiguration");
        }

        System.Data.Common.DbTransaction currentTransaction;

        #region IUnitOfWork Members

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (this.currentTransaction == null)
            {
                currentTransaction = this.dataContext.Database.Connection.BeginTransaction(isolationLevel);
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
            if (this.dataContext != null)
            {
                this.dataContext.SaveChanges();
            }
        }

        public AnotherBlogDataContext DataContext
        {
            get
            {
                if (this.dataContext == null)
                {
                    this.dataContext = new AnotherBlogDataContext(UnitOfWork.dbConfiguration.ConnectionString);
                }

                return this.dataContext;
            }
            set { this.dataContext = value; }
        }

        #endregion
    }
}
