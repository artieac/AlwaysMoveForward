using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth2.Web.Code.AspNetIdentity
{
    public class RoleStore : IRoleStore<string>
    {
        public Task<IdentityResult> CreateAsync(string role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(string role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<string> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            string retVal = RoleType.Names.User;

            int targetRoleId = int.Parse(roleId);

            if(targetRoleId == (int)RoleType.Id.Administrator)
            {
                retVal = RoleType.Names.Administrator;
            }

            return Task.FromResult(retVal);
        }

        public Task<string> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            string retVal = RoleType.Id.User.ToString();

            if (normalizedRoleName == RoleType.Names.Administrator.ToUpper())
            {
                retVal = RoleType.Id.Administrator.ToString();
            }

            return Task.FromResult(retVal);
        }

        public Task<string> GetNormalizedRoleNameAsync(string role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.ToUpper());
        }

        public Task<string> GetRoleIdAsync(string role, CancellationToken cancellationToken)
        {
            return this.FindByNameAsync(role, cancellationToken);
        }

        public Task<string> GetRoleNameAsync(string role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role);
        }

        public Task SetNormalizedRoleNameAsync(string role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(string role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(string role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
