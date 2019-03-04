using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PucksAndProgramming.Common.DataLayer.NHibernate
{
    /// <summary>
    /// An interface to define common functions for an NHibernate repository
    /// </summary>
    /// <typeparam name="TDomainType">The domain type returned by the repository</typeparam>
    public interface INHibernateRepository<TDomainType, TIdType> : IRepository<TDomainType, TIdType> where TDomainType : class
    {       

    }
}
