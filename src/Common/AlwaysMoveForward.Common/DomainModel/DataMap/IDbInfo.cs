using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DomainModel.DataMap
{
    public interface IDbInfo
    {
        int Version { get; set; }
    }
}
