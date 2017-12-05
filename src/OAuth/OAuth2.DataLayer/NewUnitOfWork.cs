using AlwaysMoveForward.Core.DataLayer.EntityFramework;
using AlwaysMoveForward.OAuth2.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer
{
    public class NewUnitOfWork : EFUnitOfWork<AMFOAuthDbContext>
    {
        AMFOAuthDbContext dbContext = null;

        public NewUnitOfWork(string connectionString) : base(connectionString) { }

        public override AMFOAuthDbContext DataContext
        {
            get
            {
                if (this.dbContext == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<AMFOAuthDbContext>();
                    optionsBuilder.UseMySQL(this.ConnectionString);
                    dbContext = new AMFOAuthDbContext(optionsBuilder.Options);
                }

                return dbContext;
            }
            set { this.dbContext = value; }
        }
    }
}
