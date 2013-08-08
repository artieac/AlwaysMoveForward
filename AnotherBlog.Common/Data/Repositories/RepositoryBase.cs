using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using log4net;
using log4net.Config;

namespace AnotherBlog.Common.Data.Repositories
{
    public abstract class RepositoryBase<DomainType, DTOType> : IRepository<DomainType> where DomainType : class where DTOType : class
    {
         private ILog logger;

        public RepositoryBase(IUnitOfWork _unitOfWork, IRepositoryManager repositoryManager)
        {
            this.UnitOfWork = _unitOfWork;
            this.RepositoryManager = repositoryManager;
        }
        
        public ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }

        public IUnitOfWork UnitOfWork { get; set; }
        public IRepositoryManager RepositoryManager { get; set; }

        public int UnsavedId
        {
            get { return -1; }
        }

        public virtual string IdPropertyName
        {
            get{ return "Id";}
        }

        public virtual DomainType GetById(int itemId)
        {
            return this.GetByProperty(this.IdPropertyName, itemId);
        }

        public virtual DomainType GetById(int itemId, int blogId)
        {
            return this.GetByProperty(this.IdPropertyName, itemId, blogId);
        }

        public virtual DomainType Create()
        {
            return Activator.CreateInstance<DTOType>() as DomainType;
        }

        public abstract DomainType GetByProperty(string idPropertyName, object idValue);
        public abstract DomainType GetByProperty(string idPropertyName, object idValue, int blogId);
        public abstract IList<DomainType> GetAll();
        public abstract IList<DomainType> GetAll(int blogId);
        public abstract IList<DomainType> GetAllByProperty(string idPropertyName, object idValue);
        public abstract IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, int blogId);
        public abstract DomainType Save(DomainType itemToSave);
        public abstract bool Delete(DomainType itemToDelete);
    }
}
