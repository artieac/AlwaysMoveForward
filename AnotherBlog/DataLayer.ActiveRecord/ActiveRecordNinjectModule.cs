using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.DataLayer
{
    public class ActiveRecordNinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IRepositoryManager>().To<RepositoryManager>();
            Bind<IAnotherBlogRepositoryManager>().To<RepositoryManager>();
        }
    }
}
