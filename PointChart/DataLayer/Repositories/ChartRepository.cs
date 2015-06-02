using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class ChartRepository : NHibernateRepository<Chart, DTO.Chart, long>, IChartRepository
    {
        public ChartRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DTO.Chart GetDTOById(Chart domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override DTO.Chart GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<DTO.Chart>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<Chart, DTO.Chart> GetDataMapper()
        {
            return new DataMapper.ChartDataMap();
        }

        public IList<Chart> GetByCreator(long creatorId)
        {
            IList<DTO.Chart> retVal = this.UnitOfWork.CurrentSession.Query<DTO.Chart>()
                .Where(r => r.CreatorId == creatorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Chart> GetByPointEarner(long pointEarnerId)
        {
            IList<DTO.Chart> retVal = this.UnitOfWork.CurrentSession.Query<DTO.Chart>()
                .Where(r => r.PointEarner.Id == pointEarnerId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public override Chart Save(Chart itemToSave)
        {
            if (itemToSave != null && itemToSave.Tasks != null)
            {
                DTO.Chart dtoItem = this.GetDTOById(itemToSave.Id);

                if(dtoItem == null)
                {
                    dtoItem = new DTO.Chart();
                    dtoItem.PointEarner = new DTO.User();
                    dtoItem.Tasks = new List<DTO.Task>();
                }

                if (dtoItem != null)
                {
                    foreach (Task domainTask in itemToSave.Tasks)
                    {
                        if (dtoItem.Tasks.FirstOrDefault(t => t.Id == domainTask.Id) == null)
                        {
                            DTO.Task existsTest = this.UnitOfWork.CurrentSession.Query<DTO.Task>()
                                .Where(t => t.Id == domainTask.Id).FirstOrDefault();

                            if (existsTest != null)
                            {
                                dtoItem.Tasks.Add(existsTest);
                            }
                        }
                    }

                    dtoItem = this.GetDataMapper().Map(itemToSave, dtoItem);
                    dtoItem.PointEarner = this.UnitOfWork.CurrentSession.Query<DTO.User>()
                            .Where(p => p.Id == itemToSave.PointEarner.Id).FirstOrDefault();

                    if (dtoItem != null)
                    {
                        this.UnitOfWork.CurrentSession.SaveOrUpdate(dtoItem);
                        this.UnitOfWork.Flush();
                    }

                    itemToSave = this.GetDataMapper().Map(dtoItem);
                }
            }

            return itemToSave;
        }
    }
}