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
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class UserService
    {
        private const string GuestUserName = "guest";
        private static AnotherBlogUser guestUser = null;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository) : base()
        {
            this.UnitOfWork = unitOfWork;
            this.UserRepository = userRepository;
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected IUserRepository UserRepository { get; private set; }

        public bool IsValidEmail(string emailString)
        {
            return Regex.IsMatch(emailString, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

<<<<<<< HEAD
        public AnotherBlogUser Save(int userId, bool isSiteAdmin, bool isApprovedCommenter, string userAbout)
=======
        public AnotherBlogUser Login(string userName, string password)
        {
            AnotherBlogUser retVal = this.UserRepository.GetByUserNameAndPassword(userName, AlwaysMoveForward.Common.Encryption.MD5HashUtility.HashString(password));

            if (retVal != null)
            {
                if (retVal.IsActive == false)
                {
                    retVal = null;
                }
            }

            return retVal;
        }

        public AnotherBlogUser Save(string userName, string password, string email, int userId, bool isSiteAdmin, bool isApprovedCommenter, bool isActive, string userAbout, string displayName)
>>>>>>> 09e0d3d4fbf0f5643a7caf9657e6025a13eb2c07
        {
            AnotherBlogUser userToSave = null;

            if (userId != 0)
            {
                userToSave = this.UserRepository.GetById(userId);
            }

            if (userToSave == null)
            {
                userToSave = new AnotherBlogUser();
            }

            userToSave.IsSiteAdministrator = isSiteAdmin;
            userToSave.ApprovedCommenter = isApprovedCommenter;

            if (userAbout != null)
            {
                userToSave.About = Utils.StripJavascript(userAbout);
            }
            else
            {
                userToSave.About = string.Empty;
            }

<<<<<<< HEAD
=======
            if (password != string.Empty)
            {
                userToSave.Password = AlwaysMoveForward.Common.Encryption.MD5HashUtility.HashString(password);
            }

            userToSave.Email = email;

>>>>>>> 09e0d3d4fbf0f5643a7caf9657e6025a13eb2c07
            return this.UserRepository.Save(userToSave);
        }

        public void Delete(int userId)
        {
            AnotherBlogUser targetUser = this.UserRepository.GetById(userId);

            using (this.UnitOfWork.BeginTransaction())
            {
                if (targetUser != null)
                {
                    this.UserRepository.Delete(targetUser);
                    this.UnitOfWork.EndTransaction(true);
                }
            }
        }

<<<<<<< HEAD
=======
        public void SendPassword(string userEmail, EmailConfiguration emailConfig)
        {
            AnotherBlogUser changePasswordUser = this.UserRepository.GetByEmail(userEmail);

            string emailBody = "A user was not found with that email address.  Please try again.";

            if (changePasswordUser != null)
            {
                string newPassword = AnotherBlogUser.GenerateNewPassword();

                emailBody = "Sorry you had a problem entering your password, your new password is " + newPassword;
                changePasswordUser.Password = AlwaysMoveForward.Common.Encryption.MD5HashUtility.HashString(newPassword);

                this.UserRepository.Save(changePasswordUser);
            }

            EmailManager emailManager = new EmailManager(emailConfig);
            emailManager.SendEmail(emailConfig.FromAddress, userEmail, "New Password", emailBody);
        }

>>>>>>> 09e0d3d4fbf0f5643a7caf9657e6025a13eb2c07
        public AnotherBlogUser GetByEmail(string userEmail)
        {
            return this.UserRepository.GetByEmail(userEmail);
        }

        public AnotherBlogUser GetDefaultUser()
        {
            if (UserService.guestUser == null)
            {
                UserService.guestUser = this.UserRepository.GetByUserName(UserService.GuestUserName);
            }

            return UserService.guestUser;
        }

        public IList<AnotherBlogUser> GetAll()
        {
            return this.UserRepository.GetAll();
        }

        public AnotherBlogUser GetByUserName(string userName)
        {
            return this.UserRepository.GetByUserName(userName);
        }

        public AnotherBlogUser GetById(int userId)
        {
            return this.UserRepository.GetById(userId);
        }      

        public IList<AnotherBlogUser> GetBlogWriters(Blog targetBlog)
        {
            return this.UserRepository.GetBlogWriters(targetBlog.BlogId);
        }

        public AnotherBlogUser AddBlogRole(int userId, int blogId, RoleType.Id roleId)
        {
            AnotherBlogUser retVal = null;

            using (this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    retVal = this.UserRepository.GetById(userId);

                    if (retVal != null)
                    {
                        retVal.AddRole(blogId, roleId);
                    }

                    retVal = this.UserRepository.Save(retVal);
                    this.UnitOfWork.EndTransaction(true);
                }
                catch (Exception e)
                {
                    LogManager.GetLogger().Error(e);
                    this.UnitOfWork.EndTransaction(false);
                }
            }

            return retVal;
        }

        public AnotherBlogUser RemoveBlogRole(int userId, int blogId)
        {
            AnotherBlogUser retVal = null;

            using (this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    retVal = this.UserRepository.GetById(userId);

                    if (retVal != null)
                    {
                        retVal.RemoveRole(blogId);
                    }

                    retVal = this.UserRepository.Save(retVal);
                    this.UnitOfWork.EndTransaction(true);
                }
                catch (Exception e)
                {
                    LogManager.GetLogger().Error(e);
                    this.UnitOfWork.EndTransaction(false);
                }
            }

            return retVal;
        }

    }
}
