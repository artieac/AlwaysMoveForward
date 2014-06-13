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
            DataMapper.AutoMapperConfiguration.Configure();
        }

        public UnitOfWork() : base(Assembly.GetExecutingAssembly()) { }
    }
}
