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
        public static AMFUserLogin Create(string userName, string firstName, string lastName, string passwordHash)
        {
            AMFUserLogin retVal = new AMFUserLogin();
            retVal.Email = userName;
            retVal.FirstName = firstName;
            retVal.LastName = lastName;
            retVal.PasswordHint = "";            
            retVal.PasswordHash = passwordHash;

            return retVal;
        }
    }
}
