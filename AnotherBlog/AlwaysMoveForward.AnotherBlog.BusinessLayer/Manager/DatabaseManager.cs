using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class DatabaseManager
    {
        DataContextManager contextManager;

        public DataContextManager DataContext
        {
            get
            {
                if (contextManager == null)
                {
                    contextManager = new DataContextManager(EntityConfiguration.adminConnectionString);
                }

                return contextManager;
            }
        }

        public void UpdateDatabase(string databaseScriptPath)
        {
            DbInfoGateway gateway = new DbInfoGateway(this.DataContext);
            DbInfo dbInfo = gateway.GetDbInfo();

            if (dbInfo == null)
            {
                this.CreateSchema(databaseScriptPath);
                this.InitializeData(databaseScriptPath);
            }

            this.UpdateSchema(databaseScriptPath);
        }

        protected void CreateSchema(string databaseScriptPath)
        {
            string sqlScript = System.IO.File.ReadAllText(databaseScriptPath + "/AnotherBlogDb.sql");
            contextManager.DataContext.ExecuteCommand(sqlScript);
        }

        protected void InitializeData(string databaseScriptPath)
        {
            string initializationData = System.IO.File.ReadAllText(databaseScriptPath + "/AnotherBlogData.sql");
            contextManager.DataContext.ExecuteCommand(initializationData);
        }

        protected void UpdateSchema(string databaseScriptPath)
        {

        }
    }
}
