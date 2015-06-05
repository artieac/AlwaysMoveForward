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
using System.Web;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Services
{
    public class PointService
    {
        public PointService(IUnitOfWork unitOfWork, IPointsSpentRepository pointsSpentRepository, IUserRepository userRepository)
        {
            this.UnitOfWork = unitOfWork;
            this.PointsSpentRepository = pointsSpentRepository;
            this.UserRepository = userRepository;
        }

        public IUnitOfWork UnitOfWork { get; private set; }
        public IPointsSpentRepository PointsSpentRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public PointsSpent SpendPoints(long pointEarnerId, double amountSpent, DateTime dateSpent, string description)
        {
            PointsSpent retVal = null;

            PointChartUser pointEarner = this.UserRepository.GetById(pointEarnerId);

            if(pointEarnerId != null && amountSpent >= 0)
            {
                PointsSpent pointsSpent = new PointsSpent();
                pointsSpent.PointEarnerId = pointEarnerId;
                pointsSpent.Amount = amountSpent;
                pointsSpent.DateSpent = dateSpent;
                pointsSpent.Description = description;

                retVal = this.PointsSpentRepository.Save(pointsSpent);
            }

            return retVal;
        }

        public IList<PointsSpent> GetPointsSpent(long pointEarnerId)
        {
            IList<PointsSpent> retVal = new List<PointsSpent>();

            PointChartUser pointEarner = this.UserRepository.GetById(pointEarnerId);

            if (pointEarner != null)
            {
                retVal = this.PointsSpentRepository.GetByPointEarner(pointEarner);
            }

            return retVal;
        }
    }
}
