using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;


namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class PointEarnerRepository : ActiveRecordRepository<PointEarner, PointEarnerDTO>
    {
        public PointEarnerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, null)
        {

        }

        public override PointEarnerDTO Map(PointEarner source)
        {
            return DataMapper.DataMapManager.Mappers().PointEarner.Map(source);
        }

        public override PointEarner Map(PointEarnerDTO source)
        {
            return DataMapper.DataMapManager.Mappers().PointEarner.Map(source);
        }

        public PointsSpentDTO Map(PointEarnerDTO owningEarner, PointsSpent source)
        {
            return DataMapper.DataMapManager.Mappers().PointsSpent.Map(source);
        }

        public PointsSpent Map(PointEarner owningEarner, PointsSpentDTO source)
        {
            return DataMapper.DataMapManager.Mappers().PointsSpent.Map(source);
        }

        public override PointEarner Save(PointEarner itemToSave)
        {
            PointEarner retVal = null;

            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("Id", itemToSave.Id));

            PointEarnerDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindOne(criteria);

            if (dtoItem == null)
            {
                dtoItem = this.Map(itemToSave);
            }
            else
            {
                dtoItem = DataMapper.DataMapManager.Mappers().PointEarner.Map(itemToSave, dtoItem);

                if (itemToSave.PointsSpent != null)
                {
                    for (int i = 0; i < itemToSave.PointsSpent.Count; i++)
                    {
                        Boolean shouldAdd = true;

                        for (int j = 0; j < dtoItem.PointsSpent.Count; j++)
                        {
                            if (itemToSave.PointsSpent[i].Id == dtoItem.PointsSpent[j].Id)
                            {
                                shouldAdd = false;
                                break;
                            }
                        }

                        if(shouldAdd==true)
                        {
                            PointsSpentDTO pointsSpent = this.Map(dtoItem, itemToSave.PointsSpent[i]);
                            pointsSpent = ((RepositoryManager) this.RepositoryManager).PointsSpent.Save(pointsSpent);
                            dtoItem.PointsSpent.Add(pointsSpent);
                            break;
                        }
                    }

                    for (int i = dtoItem.PointsSpent.Count - 1; i > -1; i--)
                    {
                        Boolean shouldRemove = true;

                        for (int j = 0; j < itemToSave.PointsSpent.Count; j++)
                        {
                            if (itemToSave.PointsSpent[j].Id > 0)
                            {
                                if (dtoItem.PointsSpent[i].Id == itemToSave.PointsSpent[j].Id)
                                {
                                    shouldRemove = false;
                                    break;
                                }
                            }
                            else
                            {
                                // in this case its an unsaved one, so it must be new, why would we remove it.
                                shouldRemove = false;
                                break;
                            }
                        }

                        if (shouldRemove == true)
                        {
                            dtoItem.PointsSpent.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            dtoItem = this.Save(dtoItem);

            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }

        public PointEarner GetByEmail(String email, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("Email", email));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindOne(criteria));
        }

        public PointEarner GetByFirstNameLastName(String firstName, String lastName, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("FirstName", firstName));
            criteria.Add(Expression.Eq("LastName", lastName));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindOne(criteria));
        }

        public IList<PointEarner> GetAllByAdministratorId(int adminstratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("AdministratorId", adminstratorId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindAll(criteria));
        }
    }
}
