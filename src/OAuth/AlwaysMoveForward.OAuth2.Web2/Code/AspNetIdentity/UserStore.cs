using AlwaysMoveForward.OAuth2.Common.DomainModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;

namespace AlwaysMoveForward.OAuth2.Web.Code.AspNetIdentity
{
    public class UserStore : IUserPasswordStore<AMFUserLogin>
    {
        public UserStore(ServiceManagerBuilder serviceManagerBuilder)
        {
            this.ServiceManagerBuilder = serviceManagerBuilder;
        }

        private ServiceManagerBuilder ServiceManagerBuilder { get; set; }

        private IServiceManager serviceManager = null;

        private IServiceManager ServiceManager
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = this.ServiceManagerBuilder.Create();
                }

                return this.serviceManager;
            }
        }

        public Task<IdentityResult> CreateAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            IdentityResult retVal;

            AMFUserLogin newUser = this.ServiceManager.UserService.Create(user);

            if (newUser != null && newUser.Id > 0)
            {
                retVal = IdentityResult.Success;
            }
            else
            {
                retVal = IdentityResult.Failed(null);
            }

            return Task.FromResult(retVal);
        }

        public Task<IdentityResult> DeleteAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            IdentityResult retVal;

            if (this.ServiceManager.UserService.Delete(user.Id))
            {
                retVal = IdentityResult.Success;

            }
            else
            {
                retVal = IdentityResult.Failed(null);
            }

            return Task.FromResult(retVal);
        }

        public void Dispose()
        {
            
        }

        public Task<AMFUserLogin> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            long userIdValue = 0;
            AMFUserLogin retVal = null;

            if (long.TryParse(userId, out userIdValue))
            {
                retVal = this.ServiceManager.UserService.GetUserById(userIdValue);
            }

            return Task.FromResult(retVal);
        }

        public Task<AMFUserLogin> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            AMFUserLogin retVal = this.ServiceManager.UserService.GetByEmail(normalizedUserName);
            return Task.FromResult(retVal);
        }

        public Task<string> GetNormalizedUserNameAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToUpper());
        }

        public Task<string> GetUserIdAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(AMFUserLogin user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task SetUserNameAsync(AMFUserLogin user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.FromResult(true);
        }

        public Task<IdentityResult> UpdateAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            IdentityResult retVal;
           
            if (this.ServiceManager.UserService.Update(user.Id, user.FirstName, user.LastName) != null)
            {
                retVal = IdentityResult.Success;
            }
            else
            {
                retVal = IdentityResult.Failed(null);
            }

            return Task.FromResult(retVal);
        }

        public Task SetPasswordHashAsync(AMFUserLogin user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(true);
        }

        public Task<string> GetPasswordHashAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            string retVal = String.Empty;

            if (user != null)
            {
                retVal = user.PasswordHash;
            }

            return Task.FromResult(retVal);
        }

        public Task<bool> HasPasswordAsync(AMFUserLogin user, CancellationToken cancellationToken)
        {
            bool retVal = false;

            if (user != null)
            {
                if(!String.IsNullOrEmpty(user.PasswordHash))
                {
                    retVal = true;
                }
            }

            return Task.FromResult(retVal);
        }
    }
}
