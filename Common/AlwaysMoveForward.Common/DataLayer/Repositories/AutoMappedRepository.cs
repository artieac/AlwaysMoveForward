using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    public abstract class AutoMappedRepository<DomainType, DTOType> : RepositoryBase<DomainType, DTOType> where DomainType : class, new() where DTOType : class, new()
    {
        public AutoMappedRepository(IUnitOfWork _unitOfWork, IRepositoryManager repositoryManager)
            : base(_unitOfWork, repositoryManager)
        {
        }

        public DomainType Map(DTOType dtoItem)
        {
            DomainType retVal = null;

            if (dtoItem != null)
            {
                retVal = AutoMapper.Mapper.Map<DTOType, DomainType>(dtoItem);
            }

            return retVal;
        }

        public DTOType Map(DomainType domainItem)
        {
            DTOType retVal = null;

            if (domainItem != null)
            {
                retVal = AutoMapper.Mapper.Map<DomainType, DTOType>(domainItem);
            }

            return retVal;
        }

        public IList<DomainType> Map(IList<DTOType> dtoItems)
        {
            IList<DomainType> retVal = null;

            if (dtoItems != null)
            {
                retVal = AutoMapper.Mapper.Map<IList<DTOType>, IList<DomainType>>(dtoItems);
            }

            return retVal;
        }

        public IList<DTOType> Map(IList<DomainType> domainItems)
        {
            IList<DTOType> retVal = null;

            if (domainItems != null)
            {
                retVal = AutoMapper.Mapper.Map<IList<DomainType>, IList<DTOType>>(domainItems);
            }

            return retVal;
        }
    }
}
