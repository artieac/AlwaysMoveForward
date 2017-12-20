using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public interface IClientRepository
    {
        IList<Client> GetAll();

        Client GetById(long id);

        Client Save(Client newResource);

    }
}
