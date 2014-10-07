using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

using NHibernate;
using NHC = NHibernate.Cfg;

using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.Common.DataLayer.NHibernate
{
    /// <summary>
    /// A unit of work made to work with NHibernate transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// A default constructor that will result in using NHibernate configuration settings just from the .config file
        /// </summary>
        public UnitOfWork() { }

        /// <summary>
        /// A constructor that allows a connection string to be specified dynamically
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        public UnitOfWork(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// A reference to the nhibernate configuration
        /// </summary>
        private NHC.Configuration nhibernateConfig;

        /// <summary>
        /// Gets the current instance of the NHibernate configuration.  It will allocate it if one doesn't already exist
        /// </summary>
        protected NHC.Configuration NHibernateConfiguration
        {
            get
            {
                if (this.nhibernateConfig == null)
                {
                    this.nhibernateConfig = new NHC.Configuration();

                    if (!string.IsNullOrEmpty(this.ConnectionString))
                    {
                        IDictionary<string, string> properties = new Dictionary<string, string>();
                        properties.Add(ConfigurationProperties.ConnectionString, this.ConnectionString);
                        this.nhibernateConfig.SetProperties(properties);
                    }

                    this.nhibernateConfig.Configure();
                }

                return this.nhibernateConfig;
            }
        }

        /// <summary>
        /// The passed in connection string (if one was passed in)
        /// </summary>
        protected string ConnectionString { get; set; }

        /// <summary>
        /// An NHibernate session factory
        /// </summary>
        private ISessionFactory sessionFactory;

        /// <summary>
        /// The current NHibernate session
        /// </summary>
        private ISession currentSession;

        /// <summary>
        /// Gets an instance of session factory based on the nhibernate configuration
        /// </summary>
        private ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = this.NHibernateConfiguration.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        /// <summary>
        /// Gets the current NHibernate session
        /// </summary>
        public ISession CurrentSession
        {
            get
            {
                if (this.currentSession == null)
                {
                    this.StartSession();
                }

                return this.currentSession;
            }
        }

        #region IUnitOfWork Members

        /// <summary>
        /// Starts a new session
        /// </summary>
        private void StartSession()
        {
            this.currentSession = this.SessionFactory.OpenSession(); ;
        }

        /// <summary>
        /// Ends the current session
        /// </summary>
        private void EndSession()
        {
            if (this.currentSession != null)
            {
                this.currentSession.Dispose();
                this.currentSession = null;
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

        /// <summary>
        /// Ends the current transaction, if can commit is true then it commits, otherwise it rolls back
        /// </summary>
        /// <param name="canCommit">Can the current transaction be commited to the database</param>
        public void EndTransaction(bool canCommit)
        {
            if (this.currentSession != null)
            {
                if (this.currentSession.Transaction != null)
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

        /// <summary>
        /// Flush any changes to the database (if outside of a transaction, otherwise NHibernate will make sure the changes don't get written
        /// unitl the transaction is committed
        /// </summary>
        public void Flush()
        {
            if (this.currentSession != null)
            {
                this.currentSession.Flush();
            }
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
