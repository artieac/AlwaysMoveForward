using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DataLayer
{
    public abstract class DataMapBase<TDomainType, TDtoType> where TDomainType : class where TDtoType : class
    {
        public virtual TDomainType Map(TDtoType source)
        {
            return this.Map(source, null);
        }

        public virtual TDtoType Map(TDomainType source)
        {
            return this.Map(source, null);
        }

        public abstract TDomainType Map(TDtoType source, TDomainType destination);
        public abstract TDtoType Map(TDomainType source, TDtoType destination);
        
        public IList<TDomainType> Map(IEnumerable<TDtoType> source)
        {
            return this.Map(source.ToList<TDtoType>());
        }

        public IList<TDomainType> Map(IList<TDtoType> source)
        {
            IList<TDomainType> retVal = new List<TDomainType>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }

        public IList<TDtoType> Map(IEnumerable<TDomainType> source)
        {
            return this.Map(source.ToList<TDomainType>());
        }

        public IList<TDtoType> Map(IList<TDomainType> source)
        {
            IList<TDtoType> retVal = new List<TDtoType>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }

    }
}
