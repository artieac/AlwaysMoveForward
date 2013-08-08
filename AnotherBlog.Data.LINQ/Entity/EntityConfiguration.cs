using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// This class contains the connection strings neccessary for any database access.  This way it doesn't
    /// have to call out to upper layers (which it shouldn't have access to)
    /// </summary>
    public class EntityConfiguration
    {
        public static string connectionString = "";
        public static string adminConnectionString = "";
    }
}
