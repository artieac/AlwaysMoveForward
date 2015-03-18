using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Contracts;

namespace AlwaysMoveForward.OAuth.Contracts.Repositories
{
    public interface IOAuthRepository
    {
        User GetUserInfo(IOAuthToken oauthToken);
    }
}
