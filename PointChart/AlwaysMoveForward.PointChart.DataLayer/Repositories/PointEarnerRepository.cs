using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Entities;
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
            PointEarnerDTO retVal = null;

            if (source != null)
            {
                retVal = new PointEarnerDTO();
                retVal.Id = source.Id;
                retVal.UserName = source.UserName;
                retVal.Password = source.Password;
                retVal.Email = source.Email;
                retVal.FirstName = source.FirstName;
                retVal.LastName = source.LastName;
                retVal.PointsEarned = source.PointsEarned;

                if (source.PointsSpent != null)
                {
                    retVal.PointsSpent = new List<PointsSpentDTO>();
                    for (int i = 0; i < source.PointsSpent.Count; i++)
                    {
                        retVal.PointsSpent.Add(this.Map(retVal, source.PointsSpent[i]));
                    }
                }
            }

            return retVal;
        }

        public override PointEarner Map(PointEarnerDTO source)
        {
            PointEarner retVal = null;

            if (source != null)
            {
                retVal = new PointEarner();
                retVal.Id = source.Id;
                retVal.Id = source.Id;
                retVal.UserName = source.UserName;
                retVal.Password = source.Password;
                retVal.Email = source.Email;
                retVal.FirstName = source.FirstName;
                retVal.LastName = source.LastName;
                retVal.PointsEarned = source.PointsEarned;

                if(source.PointsSpent!=null)
                {
                    retVal.PointsSpent = new List<PointsSpent>();
                    for(int i = 0; i < source.PointsSpent.Count; i++)
                    {
                        retVal.PointsSpent.Add(this.Map(retVal, source.PointsSpent[i]));
                    }
                }
            }

            return retVal;
        }

        public PointsSpentDTO Map(PointEarnerDTO owningEarner, PointsSpent source)
        {
            PointsSpentDTO retVal = null;

            if (source != null)
            {
                retVal = new PointsSpentDTO();
                retVal.Id = source.Id;
                retVal.DateSpent = source.DateSpent;
                retVal.Amount = source.Amount;
                retVal.Description = source.Description;
                retVal.PointEarner = owningEarner;
            }

            return retVal;
        }

        public PointsSpent Map(PointEarner owningEarner, PointsSpentDTO source)
        {
            PointsSpent retVal = null;

            if (source != null)
            {
                retVal = new PointsSpent();
                retVal.Id = source.Id;
                retVal.DateSpent = source.DateSpent;
                retVal.Amount = source.Amount;
                retVal.Description = source.Description;
                retVal.PointEarner = owningEarner;
            }

            return retVal;
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
                dtoItem.UserName = itemToSave.UserName;
                dtoItem.Password = itemToSave.Password;
                dtoItem.Email = itemToSave.Email;
                dtoItem.FirstName = itemToSave.FirstName;
                dtoItem.LastName = itemToSave.LastName;
                dtoItem.PointsEarned = itemToSave.PointsEarned;

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
