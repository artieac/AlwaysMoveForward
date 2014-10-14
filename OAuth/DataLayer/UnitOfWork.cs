﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Transactions;
using NHibernate;
using NHC = NHibernate.Cfg;
using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.OAuth.DataLayer
{
    /// <summary>
    /// A unit of work implementation to co locate the NHibernate configuration with the DTOs
    /// </summary>
    public class UnitOfWork : AlwaysMoveForward.Common.DataLayer.NHibernate.UnitOfWork, IUnitOfWork, IDisposable
    {
        private static bool nhibernateInitailized = false;

         /// <summary>
        /// The default constructor
        /// </summary>
        public UnitOfWork() : base() 
        {
            if (UnitOfWork.nhibernateInitailized == false)
            {
                UnitOfWork.nhibernateInitailized = true;

                // Enable validation (optional)
                // Here, we serialize all decorated classes (but you can also do it class by class)
                NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true;
                this.NHibernateConfiguration.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly()));
            }
        }

        /// <summary>
        /// A constructor that takes database connection strings.  These need to go away long term.
        /// </summary>
        /// <param name="connectionString">The connection string for the database</param>
        public UnitOfWork(string connectionString)
            : base(connectionString)
        {
            if (UnitOfWork.nhibernateInitailized == false)
            {
                UnitOfWork.nhibernateInitailized = true;

                // Enable validation (optional)
                // Here, we serialize all decorated classes (but you can also do it class by class)
                NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true;
                this.NHibernateConfiguration.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly()));
            }
        }
    }
}
