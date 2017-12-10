using AlwaysMoveForward.Core.Common.DataLayer.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer
{
    public class UnitOfWork : DapperUnitOfWork
    {
        public UnitOfWork(string connectionString) : base(connectionString) { }

        protected override void StartSession()
        {
            if (this.currentSession == null)
            {
//                this.currentSession = new MySqlConnection(this.ConnectionString);
            }
        }
    }
}
