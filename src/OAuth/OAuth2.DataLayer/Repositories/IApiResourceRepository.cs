using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public interface IApiResourceRepository
    {
        IList<ApiResources> GetAll();

        ApiResources GetById(long id);

        ApiResources GetByName(string name);

        IList<ApiResources> GetByScopes(IList<string> scopeNames);

        ApiResources Save(ApiResources newResource);
    }
}
