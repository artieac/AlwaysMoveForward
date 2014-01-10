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
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class UserService
    {
        private static String GuestUserName = "guest";
        private static User GuestUser = null;

        public UserService(ServiceContext serviceContext)
        {
            this.UnitOfWork = serviceContext.UnitOfWork;
            this.Repositories = serviceContext.RepositoryManager;
        }

        private IUnitOfWork UnitOfWork { get; set; }
        protected IRepositoryManager Repositories { get; private set; }

        private string GenerateNewPassword()
        {
            string retVal = "";
            Random random = new Random();
            string legalChars = "abcdefghijklmnopqrstuvwxzyABCDEFGHIJKLMNOPQRSTUVWXZY1234567890";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(legalChars.Substring(random.Next(0, legalChars.Length - 1), 1));
            }
           
            retVal = sb.ToString();

            return retVal;
        }

        public User Create()
        {
            User retVal = new User();
            retVal.UserId = this.Repositories.Users.UnsavedId;
            retVal.IsActive = true;
            return retVal;
        }

        public bool IsValidEmail(string emailString)
        {
            return Regex.IsMatch(emailString, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public User GetDefaultUser()
        {
            if (UserService.GuestUser == null)
            {
                UserService.GuestUser = Repositories.Users.GetByUserName(UserService.GuestUserName);
            }

            return UserService.GuestUser;
        }

        public User Login(string userName, string password)
        {
            User retVal = Repositories.Users.GetByUserNameAndPassword(userName, AlwaysMoveForward.Common.Encryption.EncryptionUtilities.MD5HashString(password));

            if (retVal != null)
            {
                if (retVal.IsActive == false)
                {
                    retVal = null;
                }
            }

            return retVal;
        }

        public User Save(string userName, string password, string email, int userId, bool isSiteAdmin, bool isApprovedCommenter, bool isActive, string userAbout, string displayName)
        {
            User userToSave = null;

            if (userId != 0)
            {
                userToSave = Repositories.Users.GetById(userId);
            }

            if(userToSave==null)
            {
                userToSave = this.Create();
            }

            userToSave.UserName = userName;
            userToSave.IsSiteAdministrator = isSiteAdmin;
            userToSave.ApprovedCommenter = isApprovedCommenter;
            userToSave.IsActive = isActive;
            userToSave.DisplayName = displayName;

            if (userAbout != null)
            {
                userToSave.About = Utils.StripJavascript(userAbout);
            }
            else
            {
                userToSave.About = "";
            }

            if (password != "")
            {
                userToSave.Password = AlwaysMoveForward.Common.Encryption.EncryptionUtilities.MD5HashString(password);
            }

            userToSave.Email = email;

            return Repositories.Users.Save(userToSave);
        }


        public IList<User> GetAll()
        {
            return Repositories.Users.GetAll();
        }

        public User GetByUserName(string userName)
        {
            return Repositories.Users.GetByUserName(userName);
        }

        public User GetById(int userId)
        {
            return Repositories.Users.GetById(userId);
        }

        public void Delete(int userId)
        {
            User targetUser = Repositories.Users.GetById(userId);

            using (this.UnitOfWork.BeginTransaction())
            {
                if (targetUser != null)
                {
                    Repositories.Users.DeleteDependencies(targetUser);
                    Repositories.Users.Delete(targetUser);
                    this.UnitOfWork.EndTransaction(true);
                }
            }
        }

        public void SendPassword(string userEmail, EmailConfiguration emailConfig)
        {
            User changePasswordUser = Repositories.Users.GetByEmail(userEmail);

            string emailBody = "A user was not found with that email address.  Please try again.";

            if (changePasswordUser != null)
            {
                string newPassword = this.GenerateNewPassword();

                emailBody = "Sorry you had a problem entering your password, your new password is " + newPassword;
                changePasswordUser.Password = AlwaysMoveForward.Common.Encryption.EncryptionUtilities.MD5HashString(newPassword);

                Repositories.Users.Save(changePasswordUser);
            }

            EmailManager emailManager = new EmailManager(emailConfig);
            emailManager.SendEmail(emailConfig.FromAddress, userEmail, "New Password", emailBody);
        }

        public User GetByEmail(string userEmail)
        {
            return Repositories.Users.GetByEmail(userEmail);
        }
    }
}
