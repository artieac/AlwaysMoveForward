using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PucksAndProgramming.Common.DomainModel;

namespace PucksAndProgramming.OAuth.Client
{
    public interface IOAuthRepository
    {
        User GetUserInfo(IOAuthToken oauthToken);

        User GetById(IOAuthToken oauthToken, long id);

        IList<User> GetByEmail(IOAuthToken oauthToken, string emailAddress);
    }
}
