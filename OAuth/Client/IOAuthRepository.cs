using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Client
{
    public interface IOAuthRepository
    {
        User GetUserInfo(IOAuthToken oauthToken);
    }
}
