using AlwaysMoveForward.Core.Common.DataLayer;
using AlwaysMoveForward.Core.Common.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Core.Common.DataLayer.Dapper
{
    /// <summary>
    /// A unit of work implementation to co locate the NHibernate configuration with the DTOs
    /// </summary>
    public abstract class DapperUnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public DapperUnitOfWork()
        {

        }

        /// <summary>
        /// A constructor that takes database connection strings to vistaprint.  These need to go away long term.
        /// </summary>
        /// <param name="connectionString">The connection string for the vistaprint database</param>
        public DapperUnitOfWork(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        protected IDbConnection currentSession; 

        public IDbConnection CurrentSession
        {
            get
            {
                if (this.currentSession == null)
                {
                    this.StartSession();
                }

                return this.currentSession;
            }
            set
            {
                this.currentSession = value;
            }
        }

        private IDbTransaction CurrentTransaction { get; set; }

        #region IUnitOfWork Members

        /// <summary>
        /// Starts a new session
        /// </summary>
        protected abstract void StartSession();

        /// <summary>
        /// Ends the current session
        /// </summary>
        private void EndSession()
        {
            if (this.currentSession != null)
            {
                this.currentSession.Dispose();
            }
        }


        /// <summary>
        /// Begins a transaction with a default isolation level
        /// </summary>
        /// <returns>A reference to the transaction as an IDisposable</returns>
        public IDisposable BeginTransaction()
        {
            return this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Begins a transaction with a specific isolation level
        /// </summary>
        /// <param name="isolationLevel">The isolation level for the transaction</param>
        /// <returns>A reference to the transaction as an IDisposable</returns>
        public IDisposable BeginTransaction(IsolationLevel isolationLevel)
        {
            IDisposable retVal = null;

            if (this.CurrentSession != null)
            {
                retVal = this.CurrentTransaction = this.CurrentSession.BeginTransaction(isolationLevel);
            }
            else
            {
                LogManager.CreateLogger<DapperUnitOfWork>().LogError("Unable to start transaction, no session established.");
            }

            return retVal;
        }

        /// <summary>
        /// Ends the current transaction, if can commit is true then it commits, otherwise it rolls back
        /// </summary>
        /// <param name="canCommit">Can the current transaction be commited to the database</param>
        public void EndTransaction(bool canCommit)
        {
            if (this.CurrentSession != null)
            {
                if (this.CurrentTransaction != null)
                {
                    if (canCommit)
                    {
                        this.CurrentTransaction.Commit();
                    }
                    else
                    {
                        this.CurrentTransaction.Rollback();
                    }

                    this.CurrentTransaction.Dispose();
                    this.CurrentTransaction = null;
                }
            }
            else
            {
                LogManager.CreateLogger<DapperUnitOfWork>().LogError("Unable to end transaction, no session established.");
            }
        }

        /// <summary>
        /// Flush any changes to the database (if outside of a transaction, otherwise NHibernate will make sure the changes don't get written
        /// unitl the transaction is committed
        /// </summary>
        public void Flush()
        {

        }

        #endregion

        /// <summary>
        /// Dispose of the elements contained in this class
        /// </summary>
        public void Dispose()
        {
            this.EndTransaction(true);
            this.EndSession();
        }
    }
}
