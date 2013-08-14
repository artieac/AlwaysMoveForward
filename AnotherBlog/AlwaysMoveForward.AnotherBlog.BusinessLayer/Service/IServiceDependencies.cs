using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public interface IServiceDependencies
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
