using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using Ninject.Modules;

namespace AlwaysMoveForward.AnotherBlog.DataLayer
{
    public class NHibernateNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IRepositoryManager>().To<RepositoryManager>();
        }
    }
}
