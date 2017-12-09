using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public interface IApiResourceRepository
    {
        IList<ProtectedApiResource> GetAll();

        ProtectedApiResource GetById(long id);

        ProtectedApiResource GetByName(string name);

        IList<ProtectedApiResource> GetByScopes(IList<string> scopeNames);

        ProtectedApiResource Save(ProtectedApiResource newResource);
    }
}
