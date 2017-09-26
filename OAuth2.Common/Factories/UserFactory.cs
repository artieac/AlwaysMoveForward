using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth2.Common.Factories
{
    public class UserFactory
    {
        public static AMFUserLogin Create(string userName, string password, string firstName, string lastName, string passwordHint)
        {
            AMFUserLogin retVal = new AMFUserLogin();
            retVal.Email = userName;
            retVal.FirstName = firstName;
            retVal.LastName = lastName;
            retVal.PasswordHint = passwordHint;
            retVal.UpdatePassword(password);

            return retVal;
        }
    }
}
