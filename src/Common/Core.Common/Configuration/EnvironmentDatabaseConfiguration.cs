using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.Core.Common.Configuration
{
    public class EnvironmentDatabaseConfiguration : DatabaseConfiguration
    {
        public EnvironmentDatabaseConfiguration()
        {
            this.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            this.DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
        }
    }
}
