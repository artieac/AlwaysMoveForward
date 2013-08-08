using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Core.Entity;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core
{
    public class UserManager : ServiceBase
    {
        private static User GuestUser = null;

        public UserManager(ModelContext managerContext)
            : base(managerContext)
        {

        }

        private string EncryptString(string inVal)
        {
            string retVal = "";

            MD5CryptoServiceProvider md5Service = new MD5CryptoServiceProvider();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(inVal);
            byte[] hash = md5Service.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            retVal = sb.ToString();

            return retVal;
        }

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
            retVal.IsActive = true;
            return retVal;
        }

        public User GetDefaultUser()
        {
            if (UserManager.GuestUser == null)
            {
                UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
                UserManager.GuestUser = gateway.GetByUserName("guest");
            }

            return UserManager.GuestUser;
        }

        public User Login(string userName, string password)
        {
            User retVal = null;

            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
            retVal = gateway.GetByUserNameAndPassword(userName, this.EncryptString(password));

            if (retVal != null)
            {
                if (retVal.IsActive == false)
                {
                    retVal = null;
                }
                else
                {
                    retVal.IsAuthenticated = true;
                }
            }

            return retVal;
        }

        public User Save(string userName, string password, string email, int userId, bool isSiteAdmin, bool isApprovedCommenter, bool isActive, string userAbout, string displayName)
        {
            User userToSave = null;
            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);

            if (userId != 0)
            {
                userToSave = gateway.GetById(userId);
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
                userToSave.Password = this.EncryptString(password);
            }

            userToSave.Email = email;

            gateway.Save(userToSave, true);

            return userToSave;
        }


        public PagedList<User> GetAll(int currentPageIndex)
        {
            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
            return Pagination.ToPagedList(gateway.GetAll(), currentPageIndex, Constants.PageSize);
        }

        public User GetByUserName(string userName)
        {
            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
            return gateway.GetByUserName(userName);
        }

        public User GetById(int userId)
        {
            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
            return gateway.GetById(userId);
        }

        public void SendPassword(string userEmail, EmailConfiguration emailConfig)
        {
            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
            User changePasswordUser = gateway.GetByEmail(userEmail);

            string emailBody = "A user was not found with that email address.  Please try again.";

            if (changePasswordUser != null)
            {
                string newPassword = this.GenerateNewPassword();

                emailBody = "Sorry you had a problem entering your password, your new password is " + newPassword;
                changePasswordUser.Password = this.EncryptString(newPassword);

                gateway.Save(changePasswordUser, true);
            }

            EmailManager emailManager = new EmailManager(emailConfig);
            emailManager.SendEmail(emailConfig.FromAddress, userEmail, "New Password", emailBody);
        }

        public User GetByEmail(string userEmail)
        {
            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
            return gateway.GetByEmail(userEmail);
        }

        public List<User> GetBlogWriters(int blogId)
        {
            UserGateway gateway = new UserGateway(this.ModelContext.DataContext);
            return gateway.GetBlogWriters(blogId);
        }
    }
}
