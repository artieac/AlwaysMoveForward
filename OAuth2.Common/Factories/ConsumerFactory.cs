using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth2.Common.Factories
{
    public class ConsumerFactory
    {
        public static Consumer Create(string consumerName, string contactEmail)
        {
            Consumer retVal = new Consumer();

            if (!string.IsNullOrEmpty(consumerName) && !string.IsNullOrEmpty(contactEmail))
            {
                retVal.ConsumerKey = Guid.NewGuid().ToString();
                retVal.ConsumerSecret = Guid.NewGuid().ToString();
                retVal.Name = consumerName;
                retVal.ContactEmail = contactEmail;
                retVal.PublicKey = string.Empty;
            }
            else
            {
                throw new ArgumentException();
            }

            return retVal;
        }
    }
}
