﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Transactions;
using NHibernate;
using NHC = NHibernate.Cfg;
using VP.Digital.Common.DataLayer;

namespace VP.Digital.Security.OAuth.DataLayer
{
    /// <summary>
    /// A unit of work implementation to co locate the NHibernate configuration with the DTOs
    /// </summary>
    public class UnitOfWork : VP.Digital.Common.DataLayer.NHibernate.UnitOfWork, IUnitOfWork, IDisposable
    {
         /// <summary>
        /// The default constructor
        /// </summary>
        public UnitOfWork() : base() 
        {
            // Enable validation (optional)
            // Here, we serialize all decorated classes (but you can also do it class by class)
            NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true; 
            this.NHibernateConfiguration.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly()));
        }

        /// <summary>
        /// A constructor that takes database connection strings to vistaprint.  These need to go away long term.
        /// </summary>
        /// <param name="vistaprintConnectionString">The connection string for the vistaprint database</param>
        public UnitOfWork(string connectionString)
            : base(connectionString)
        {
            // Enable validation (optional)
            // Here, we serialize all decorated classes (but you can also do it class by class)
            NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true; 
            this.NHibernateConfiguration.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly()));
        }
    }
}
