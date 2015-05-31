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
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Services
{
    public class UserService
    {
        private const string GuestUserName = "guest";
        private static PointChartUser guestUser = null;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IOAuthRepository oauthRepository)
            : base()
        {
            this.UnitOfWork = unitOfWork;
            this.UserRepository = userRepository;
            this.OAuthRepository = oauthRepository;
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected IUserRepository UserRepository { get; private set; }

        protected IOAuthRepository OAuthRepository { get; private set; }

        public PointChartUser Save(long userId, bool isSiteAdmin, bool isApprovedCommenter, string userAbout)
        {
            PointChartUser userToSave = null;

            if (userId != 0)
            {
                userToSave = this.UserRepository.GetById(userId);
            }

            if (userToSave == null)
            {
                userToSave = new PointChartUser();
            }

            userToSave.IsSiteAdministrator = isSiteAdmin;

            return this.UserRepository.Save(userToSave);
        }

        public PointChartUser Save(PointChartUser user)
        {
            if (user != null)
            {
                user = this.UserRepository.Save(user);
            }

            return user;
        }
        public void Delete(long userId)
        {
            PointChartUser targetUser = this.UserRepository.GetById(userId);

            using (this.UnitOfWork.BeginTransaction())
            {
                if (targetUser != null)
                {
                    this.UserRepository.Delete(targetUser);
                    this.UnitOfWork.EndTransaction(true);
                }
            }
        }

        public PointChartUser GetDefaultUser()
        {
            if (UserService.guestUser == null)
            {
                UserService.guestUser = new PointChartUser();
                guestUser.IsSiteAdministrator = false;
            }

            return UserService.guestUser;
        }

        public IList<PointChartUser> GetAll()
        {
            return this.UserRepository.GetAll();
        }

        public PointChartUser GetById(long userId)
        {
            return this.UserRepository.GetById(userId);
        }

        public PointChartUser GetFromAMFUser(IOAuthToken accessToken)
        {
            PointChartUser retVal = null;

            AlwaysMoveForward.Common.DomainModel.User amfUser = this.GetAMFUserInfo(accessToken);

            if (amfUser != null)
            {
                retVal = this.UserRepository.GetByOAuthServiceUserId(amfUser.Id);

                if (retVal == null)
                {
                    retVal = new PointChartUser(amfUser);
                }

                ((IRemoteOAuthUser)retVal).AccessToken = accessToken.Token;
                ((IRemoteOAuthUser)retVal).AccessTokenSecret = accessToken.Secret;
                retVal = this.UserRepository.Save(retVal);
            }

            return retVal;
        }

        public User GetAMFUserInfo(IOAuthToken oauthToken)
        {
            return this.OAuthRepository.GetUserInfo(oauthToken);
        }

        public PointChartUser FindByEmail(string emailAddress, IOAuthToken oauthToken)
        {
            PointChartUser retVal = null;

            if(!string.IsNullOrEmpty(emailAddress))
            {
                IList<User> foundUsers = this.OAuthRepository.GetByEmail(oauthToken, emailAddress);

                if(foundUsers != null && foundUsers.Count == 1)
                {
                    retVal = this.UserRepository.GetByOAuthServiceUserId(foundUsers[0].Id);

                    if(retVal == null)
                    {
                        retVal = this.UserRepository.Save(new PointChartUser(foundUsers[0]));
                    }
                }
            }

            return retVal;
        }

        public IList<PointChartUser> SearchByEmail(string emailAddress, IOAuthToken oauthToken)
        {
            IList<PointChartUser> retVal = new List<PointChartUser>();

            if (!string.IsNullOrEmpty(emailAddress))
            {
                IList<User> foundUsers = this.OAuthRepository.GetByEmail(oauthToken, emailAddress);

                for(int i = 0; i < foundUsers.Count; i++)
                {
                    retVal.Add(new PointChartUser(foundUsers[i]));
                }
            }

            return retVal;
        }

        public PointChartUser AddNewPointEarner(long oauthServiceId, IOAuthToken oauthToken, PointChartUser currentUser)
        {
            PointChartUser retVal = currentUser;

            if(currentUser != null)
            {
                PointChartUser pointEarner = new PointChartUser(this.OAuthRepository.GetById(oauthToken, oauthServiceId));
                
                if(pointEarner!=null)
                {
                    PointChartUser alreadyInList = currentUser.PointEarners
                        .Where(e => e.OAuthServiceUserId == oauthServiceId)
                        .FirstOrDefault();
                    
                    if(alreadyInList==null)
                    {
                        retVal.PointEarners.Add(pointEarner);
                        retVal = this.UserRepository.Save(retVal);
                    }
                }
            }

            return retVal;
        }

        public PointChartUser AddExistingPointEarner(long pointEarnerId, PointChartUser currentUser)
        {
            PointChartUser retVal = currentUser;

            if(currentUser != null)
            {
                PointChartUser pointEarner = this.UserRepository.GetById(pointEarnerId);

                if(pointEarner!=null)
                {
                    PointChartUser alreadyInList = currentUser.PointEarners
                        .Where(e => e.Id == pointEarnerId)
                        .FirstOrDefault();
                    
                    if(alreadyInList==null)
                    {
                        retVal.PointEarners.Add(pointEarner);
                        retVal = this.UserRepository.Save(retVal);
                    }
                }
            }

            return retVal;
        }

        public bool RemovePointEarner(long id, PointChartUser currentUser)
        {
            bool retVal = false;

            if(currentUser != null)
            {
                for(int i = 0; i < currentUser.PointEarners.Count; i++)
                {
                    if(currentUser.PointEarners[i].Id == id)
                    {
                        currentUser.PointEarners.RemoveAt(i);
                        retVal = true;
                        break;
                    }
                }
            }

            if(retVal == true)
            {
                this.UserRepository.Save(currentUser);
            }

            return retVal;
        }
    }
}
