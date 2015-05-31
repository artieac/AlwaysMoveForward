/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class UserRepository : NHibernateRepository<PointChartUser, DTO.User, long>, IUserRepository
    {
        public UserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DTO.User GetDTOById(PointChartUser domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override DTO.User GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<DTO.User>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<PointChartUser, DTO.User> GetDataMapper()
        {
            return new DataMapper.PointChartUserDataMap();
        }

        public DTO.User GetDTOByOAuthServiceUserId(long oauthServiceId)
        {
            return this.UnitOfWork.CurrentSession.Query<DTO.User>()
                .Where(r => r.OAuthServiceUserId == oauthServiceId)
                .FirstOrDefault();
        }

        public PointChartUser GetByOAuthServiceUserId(long oauthServiceId)
        {
            return this.GetDataMapper().Map(this.GetDTOByOAuthServiceUserId(oauthServiceId));
        }

        public override PointChartUser Save(PointChartUser itemToSave)
        {
            if (itemToSave != null)
            {
                DTO.User dtoItem = this.GetDTOById(itemToSave.Id);

                if(dtoItem == null)
                {
                    dtoItem = this.GetDTOByOAuthServiceUserId(itemToSave.OAuthServiceUserId);
                }

                if (dtoItem != null)
                {
                    if (itemToSave.PointEarners != null)
                    {
                        foreach (PointChartUser domainListItem in itemToSave.PointEarners)
                        {
                            if (dtoItem.PointEarners.FirstOrDefault(t => t.Id == domainListItem.Id) == null)
                            {
                                DTO.User existsTest = this.UnitOfWork.CurrentSession.Query<DTO.User>()
                                    .Where(t => t.Id == domainListItem.Id).FirstOrDefault();

                                if (existsTest != null)
                                {
                                    dtoItem.PointEarners.Add(existsTest);
                                }
                            }
                        }
                    }
                }
            }

            return base.Save(itemToSave);
        }
    }
}
