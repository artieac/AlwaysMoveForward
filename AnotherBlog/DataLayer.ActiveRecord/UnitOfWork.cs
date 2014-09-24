using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;

namespace AlwaysMoveForward.AnotherBlog.DataLayer
{
    public class UnitOfWork : ActiveRecordUnitOfWork
    {
        static UnitOfWork()
        {

        }

        public UnitOfWork() : base(Assembly.GetExecutingAssembly()) { }

        public UnitOfWork(bool createSession) : base(Assembly.GetExecutingAssembly(), createSession) { }
    }
}
