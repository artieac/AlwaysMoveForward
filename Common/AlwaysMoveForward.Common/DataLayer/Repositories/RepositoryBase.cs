using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using AlwaysMoveForward.Common.Utilities;

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    public abstract class RepositoryBase<DomainType, DTOType> 
        : IRepository<DomainType> 
        where DomainType  : class, new()
        where DTOType : class, new()
    {
        public RepositoryBase(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            this.UnitOfWork = unitOfWork;
            this.RepositoryManager = repositoryManager;
        }

        public virtual DomainType Create()
        {
            return new DomainType();
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

        public abstract DomainType GetByProperty(string idPropertyName, object idValue);
        public abstract DomainType GetByProperty(string idPropertyName, object idValue, int blogId);
        public abstract IList<DomainType> GetAll();
        public abstract IList<DomainType> GetAll(int blogId);
        public abstract IList<DomainType> GetAllByProperty(string idPropertyName, object idValue);
        public abstract IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, int blogId);
        public abstract DomainType Save(DomainType itemToSave);
        public abstract bool Delete(DomainType itemToDelete);

        public virtual bool DeleteDependencies(DomainType parentItem) { return true; }
    }
}
