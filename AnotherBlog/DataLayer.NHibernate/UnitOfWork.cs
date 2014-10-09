using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.AnotherBlog.DataLayer
{
    /// <summary>
    /// A unit of work implementation to co locate the NHibernate configuration with the DTOs
    /// </summary>
    public class UnitOfWork : AlwaysMoveForward.Common.DataLayer.NHibernate.UnitOfWork, IUnitOfWork, IDisposable
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public UnitOfWork()
            : base()
        {
            // Enable validation (optional)
            // Here, we serialize all decorated classes (but you can also do it class by class)
            NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true;
            this.NHibernateConfiguration.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly()));
        }

        /// <summary>
        /// A constructor that takes database connection strings.  
        /// </summary>
        /// <param name="connectionString">The connection string for the database</param>
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
