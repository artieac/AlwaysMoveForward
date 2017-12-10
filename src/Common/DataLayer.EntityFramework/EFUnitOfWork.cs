using AlwaysMoveForward.Core.Common.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AlwaysMoveForward.Core.DataLayer.EntityFramework
{
    public abstract class EFUnitOfWork<TDataContext> : IUnitOfWork where TDataContext : DbContext
    {
        protected TDataContext dataContext;
        private TransactionScope currentTransaction;

        public EFUnitOfWork() { }
        /// <summary>
        /// A constructor that takes database connection strings to vistaprint.  These need to go away long term.
        /// </summary>
        /// <param name="connectionString">The connection string for the vistaprint database</param>
        public EFUnitOfWork(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        #region IUnitOfWork Members

        public IDisposable BeginTransaction()
        {
            return this.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        public IDisposable BeginTransaction(System.Data.IsolationLevel isolationLevel)
        {
            if (this.currentTransaction == null)
            {
                System.Transactions.IsolationLevel isoLevel = System.Transactions.IsolationLevel.Unspecified;

                switch (isolationLevel)
                {
                    case System.Data.IsolationLevel.Chaos:
                        isoLevel = System.Transactions.IsolationLevel.Chaos;
                        break;
                    case System.Data.IsolationLevel.ReadCommitted:
                        isoLevel = System.Transactions.IsolationLevel.ReadCommitted;
                        break;
                    case System.Data.IsolationLevel.ReadUncommitted:
                        isoLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                        break;
                    case System.Data.IsolationLevel.RepeatableRead:
                        isoLevel = System.Transactions.IsolationLevel.RepeatableRead;
                        break;
                    case System.Data.IsolationLevel.Serializable:
                        isoLevel = System.Transactions.IsolationLevel.Serializable;
                        break;
                    case System.Data.IsolationLevel.Snapshot:
                        isoLevel = System.Transactions.IsolationLevel.Snapshot;
                        break;
                }

                this.currentTransaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = isoLevel });
            }

            return currentTransaction;
        }

        public void EndTransaction(bool canCommit)
        {
            if (currentTransaction != null)
            {
                if (canCommit)
                {
                    currentTransaction.Complete();
                }

                currentTransaction.Dispose();
                currentTransaction = null;
            }
        }

        public void Commit()
        {
            if (this.DataContext != null)
            {
                this.DataContext.SaveChanges();
            }
        }

        public void Flush()
        {

        }

        public abstract TDataContext DataContext { get; set; }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}

