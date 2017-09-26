using AlwaysMoveForward.OAuth2.Common.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    /// <summary>
    /// This override defines the base class for the repositories.  The core feature it provides is the
    /// strongly typed DataContext to make it easier to work with the database
    /// </summary>
    /// <typeparam name="TDomainType">The domain typeto return</typeparam>
    /// <typeparam name="TDTOType">The dto type to read/write with</typeparam>
    public abstract class DapperRepositoryBase<TDomainType, TDTOType, TIdType> : RepositoryBase<UnitOfWork, TDomainType, TDTOType, TIdType>
        where TDomainType : class, new()
        where TDTOType : class, new()
    {
        /// <summary>
        /// The constructor that takes the current unit of work as a parameter
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected DapperRepositoryBase(IUnitOfWork unitOfWork, string tableName) : base(unitOfWork as UnitOfWork)
        {
            this.UnitOfWork = unitOfWork as UnitOfWork;
            this.TableName = tableName;
        }

        /// <summary>
        /// Gets the current unit of work
        /// </summary>
        public UnitOfWork UnitOfWork { get; private set; }

        protected string TableName { get; private set; } 

        public override IList<TDomainType> GetAll()
        {
            IEnumerable<TDTOType> retVal = this.UnitOfWork.CurrentSession.Query<TDTOType>("Select * FROM " + this.TableName).ToList();
            return this.GetDataMapper().Map(retVal);
        }

        public IList<TDomainType> GetAll(int pageSize, int pageIndex)
        {
            int rowStart = pageSize * pageIndex;

            string query = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, FROM " + this.TableName + ") AS result WHERE RowNum >= " + rowStart + " AND RowNum < " + (rowStart + pageSize) + " ORDER BY RowNum";
            IEnumerable<TDTOType> retVal = this.UnitOfWork.CurrentSession.Query<TDTOType>(query).ToList();
            return this.GetDataMapper().Map(retVal);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override TDTOType GetDTOById(TIdType id)
        {
            TDTOType retVal = this.UnitOfWork.CurrentSession.QueryFirstOrDefault<TDTOType>("Select * FROM " + this.TableName + " WHERE Id = @Id",
                        new { id });

            return retVal;
        }

        /// <summary>
        /// Get a Domain instance by a specific property and value
        /// </summary>
        /// <param name="idPropertyName">The property name</param>
        /// <param name="idValue">The value to search for</param>
        /// <returns>A domain object instance</returns>
        public override TDomainType GetByProperty(string idPropertyName, object propertyValue)
        {
            TDTOType retVal = this.UnitOfWork.CurrentSession.QueryFirstOrDefault<TDTOType>("Select * FROM " + this.TableName + " WHERE " + idPropertyName + " = @propertValue",
                        new { propertyValue });

            return this.GetDataMapper().Map(retVal);
        }

        /// <summary>
        /// Get all records that have a property with a specific value
        /// </summary>
        /// <param name="idPropertyName">The name of the property</param>
        /// <param name="idValue">The value of the property</param>
        /// <returns>A list of domain objects</returns>
        public override IList<TDomainType> GetAllByProperty(string idPropertyName, object propertyValue)
        {
            IEnumerable<TDTOType> retVal = this.UnitOfWork.CurrentSession.Query<TDTOType>("Select * FROM " + this.TableName + " WHERE " + idPropertyName + " = @propertyValue",
                        new { propertyValue }).ToList();

            return this.GetDataMapper().Map(retVal);
        }

        /// <summary>
        /// Save the object to the data store
        /// </summary>
        /// <param name="itemToSave">The item values to save</param>
        /// <returns>The saved domain object</returns>
        public TDomainType Add(TDomainType itemToSave)
        {
            return this.Save(itemToSave);
        }

        /// <summary>
        /// Save the object to the data store
        /// </summary>
        /// <param name="itemToSave">The item values to save</param>
        /// <returns>The saved domain object</returns>
        public TDomainType Update(TDomainType itemToSave)
        {
            return this.Save(itemToSave);
        }

        /// <summary>
        /// Save the object to the data store
        /// </summary>
        /// <param name="itemToSave">The item values to save</param>
        /// <returns>The saved domain object</returns>
        public override TDomainType Save(TDomainType itemToSave)
        {
            if (itemToSave != null)
            {
                TDTOType dtoItemToSave = this.GetDTOById(itemToSave);

                if (dtoItemToSave != null)
                {
                    dtoItemToSave = this.GetDataMapper().Map(itemToSave, dtoItemToSave);
                    this.UnitOfWork.CurrentSession.Update(dtoItemToSave);
                }
                else
                {
                    dtoItemToSave = this.GetDataMapper().Map(itemToSave);
                    this.UnitOfWork.CurrentSession.Insert(dtoItemToSave);
                }

                if (dtoItemToSave != null)
                {
                    this.UnitOfWork.Flush();
                }

                itemToSave = this.GetDataMapper().Map(dtoItemToSave);
            }

            return itemToSave;
        }

        /// <summary>
        /// Remove the object from the data store
        /// </summary>
        /// <param name="itemToDelete">The object to delete</param>
        public override bool Delete(TDomainType itemToDelete)
        {
            bool retVal = false;

            if (itemToDelete != null)
            {
                TDTOType dtoItemToDelete = this.GetDTOById(itemToDelete);

                if (dtoItemToDelete != null)
                {
//                    this.UnitOfWork.CurrentSession.Delete<TDTOType>(dtoItemToDelete);
                    this.UnitOfWork.Flush();
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}
