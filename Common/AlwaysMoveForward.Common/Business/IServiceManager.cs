using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.Common.Business
{
    public interface IServiceManager
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
