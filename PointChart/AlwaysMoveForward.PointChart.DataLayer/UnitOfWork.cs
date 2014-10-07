using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;

namespace AlwaysMoveForward.PointChart.DataLayer
{
    public class UnitOfWork : ActiveRecordUnitOfWork
    {
        public UnitOfWork() : base(Assembly.GetExecutingAssembly()) { }
    }
}
