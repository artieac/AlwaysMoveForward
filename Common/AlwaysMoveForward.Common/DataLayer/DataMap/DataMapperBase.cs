using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DataLayer.DataMap
{
    public abstract class DataMapperBase<TDomainType, TDTOType>
        where TDomainType : class, new()
        where TDTOType : class, new()
    {
        public abstract TDomainType Map(TDTOType source);
        public abstract TDTOType Map(TDomainType source);

        public virtual IList<TDomainType> Map(IList<TDTOType> source)
        {
            IList<TDomainType> retVal = new List<TDomainType>();

            if(source!=null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }

        public virtual IList<TDTOType> Map(IList<TDomainType> source)
        {
            IList<TDTOType> retVal = new List<TDTOType>();

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
