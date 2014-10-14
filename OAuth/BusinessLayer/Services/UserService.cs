using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Encryption;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    /// <summary>
    /// A service for managing user accounts
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public UserService(IDigitalUserRepository digitalUserRepository, ILoginAttemptRepository loginAttemptRepository)
        {
            this.DigitalUserRepository = digitalUserRepository;
            this.LoginAttemptRepository = loginAttemptRepository;
        }

        /// <summary>
        /// Gets and sets the contained digital user repository
        /// </summary>
        protected IDigitalUserRepository DigitalUserRepository { get; private set; }

        /// <summary>
        /// Gets and sets the contained login Attempt repository
        /// </summary>
        protected ILoginAttemptRepository LoginAttemptRepository { get; private set; }

        /// <summary>
        /// Get all of the users.
        /// </summary>
        /// <returns>A list of digital users</returns>
        public IList<AMFUserLogin> GetAll()
        {
            return this.DigitalUserRepository.GetAll();
        }

        /// <summary>
        /// Update the editable fields for a User
        /// </summary>
        /// <param name="digitalUserLogin">The source user</param>
        /// <returns>The updated user</returns>
        public AMFUserLogin Update(AMFUserLogin digitalUserLogin)
        {
            AMFUserLogin targetUser = this.DigitalUserRepository.GetById(digitalUserLogin.Id);

            if(targetUser != null)
            {
                targetUser.FirstName = digitalUserLogin.FirstName;
                targetUser.LastName = digitalUserLogin.LastName;
                targetUser.UserStatus = digitalUserLogin.UserStatus;
                targetUser.Role = digitalUserLogin.Role;

                digitalUserLogin = this.DigitalUserRepository.Save(targetUser);
            }

            return digitalUserLogin;
        }

        /// <summary>
        /// Register a user with the system
        /// </summary>
        /// <param name="userName">The username of the user</param>
        /// <param name="password">The users password</param>
        /// <param name="passwordHint">The password hint for forgotten passwords</param>
        /// <param name="firstName">The users password</param>
        /// <param name="lastName">The users last name</param>
        /// <returns>An instance of a user</returns>
        public AMFUserLogin Register(string userName, string password, string passwordHint, string firstName, string lastName)
        {
            AMFUserLogin retVal = null;

            AMFUserLogin userLogin = new AMFUserLogin();
            userLogin.Email = userName;
            userLogin.FirstName = firstName;
            userLogin.LastName = lastName;
            userLogin.PasswordHint = passwordHint;

            SHA1HashUtility passwordHashUtility = new SHA1HashUtility();
            userLogin.PasswordHash = passwordHashUtility.HashPassword(password);
            userLogin.PasswordSalt = Convert.ToBase64String(passwordHashUtility.Salt);

            retVal = this.DigitalUserRepository.Save(userLogin);

            return retVal;
        }

        /// <summary>
        /// Logon a user by the username and password
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">The unencrypted password</param>
        /// <returns>The user if one is found to match</returns>
        public AMFUserLogin LogonUser(string userName, string password, string loginSource)
        {
            AMFUserLogin retVal = null;

            AMFUserLogin targetUser = this.DigitalUserRepository.GetByEmail(userName);

            if (targetUser != null && targetUser.UserStatus == UserStatus.Active)
            {
                byte[] passwordSalt = Convert.FromBase64String(targetUser.PasswordSalt);

                if (SHA1HashUtility.ValidatePassword(password, targetUser.PasswordHash, passwordSalt, AMFUserLogin.SaltIterations) == true)
                {
                    retVal = targetUser;
                }
            }

            if (retVal == null)
            {
                this.AddLoginAttempt(false, loginSource, userName, targetUser);
            }
            else
            {
                this.AddLoginAttempt(true, loginSource, userName, targetUser);
            }

            return retVal;
        }

        public void SendPassword(string userEmail, EmailConfiguration emailConfig)
        {
            AMFUserLogin changePasswordUser = this.DigitalUserRepository.GetByEmail(userEmail);

            string emailBody = "A user was not found with that email address.  Please try again.";

            if (changePasswordUser != null)
            {
                string newPassword = AMFUserLogin.GenerateNewPassword();

                emailBody = "Sorry you had a problem entering your password, your new password is " + newPassword;
                SHA1HashUtility passwordHashUtility = new SHA1HashUtility();
                changePasswordUser.PasswordHash = passwordHashUtility.HashPassword(newPassword);
                changePasswordUser.PasswordSalt = Convert.ToBase64String(passwordHashUtility.Salt);

                this.DigitalUserRepository.Save(changePasswordUser);
            }

            EmailManager emailManager = new EmailManager(emailConfig);
            emailManager.SendEmail(emailConfig.FromAddress, userEmail, "New Password", emailBody);
        }

        /// <summary>
        /// Find a user by its id
        /// </summary>
        /// <param name="userId">The id of the user to look for</param>
        /// <returns>The user if one is found</returns>
        public AMFUserLogin GetUserById(int userId)
        {
            return this.DigitalUserRepository.GetById(userId);
        }

        /// <summary>
        /// Find a user by its email
        /// </summary>
        /// <param name="email">The email of the user to look for</param>
        /// <returns>The user if one is found</returns>
        public AMFUserLogin GetByEmail(string email)
        {
            return this.DigitalUserRepository.GetByEmail(email);
        }

        /// <summary>
        /// Update the login attempt tracking information based upon the last login status
        /// </summary>
        /// <param name="didLoginSucceed">Did the last login attempt succeed</param>
        public UserStatus AddLoginAttempt(bool didLoginSucceed, string source, string userName, AMFUserLogin relatedUser)
        {
            if (source == null)
            {
                source = string.Empty;
            }

            LoginAttempt newLoginAttempt = new LoginAttempt();
            newLoginAttempt.AttemptDate = DateTime.UtcNow;
            newLoginAttempt.WasSuccessfull = didLoginSucceed;
            newLoginAttempt.Source = source;
            newLoginAttempt.UserName = userName;

            this.LoginAttemptRepository.Save(newLoginAttempt);

            UserStatus retVal = UserStatus.Active;            

            if (didLoginSucceed == true)
            {
                retVal = UserStatus.Active;
            }
            else
            {
                int failureCount = this.GetLoginFailureCount(userName, AMFUserLogin.MaxAllowedLoginFailures);

                if (failureCount == AMFUserLogin.MaxAllowedLoginFailures)
                {
                    retVal = UserStatus.Locked;
                }
            }

            if (relatedUser != null)
            {
                relatedUser.UserStatus = retVal;
                relatedUser = this.DigitalUserRepository.Save(relatedUser);
            }

            return retVal;
        }

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <returns>The failed login count</returns>
        public int GetLoginFailureCount(string userName)
        {
            return this.GetLoginFailureCount(userName, AMFUserLogin.MaxAllowedLoginFailures);
        }

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <param name="maxItemsToCheck">The number of items to check for failure</param>
        /// <returns>The failed login count</returns>
        public int GetLoginFailureCount(string userName, int maxItemsToCheck)
        {
            int retVal = 0;

            IList<LoginAttempt> loginsForUserName = this.LoginAttemptRepository.GetByUserName(userName);

            if(loginsForUserName != null)
            {
                IEnumerable<LoginAttempt> loginFailureWindow = loginsForUserName.Take(maxItemsToCheck);

                foreach (LoginAttempt loginAttempt in loginFailureWindow)
                {
                    if (loginAttempt.WasSuccessfull == false)
                    {
                        retVal++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return retVal;
        }

        public IList<LoginAttempt> GetLoginHistory(string userName)
        {
            return this.LoginAttemptRepository.GetByUserName(userName);
        }
    }
}