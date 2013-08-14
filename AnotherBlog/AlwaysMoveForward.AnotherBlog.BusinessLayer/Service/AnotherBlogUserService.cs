using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class AnotherBlogUserService : UserService
    {
        public AnotherBlogUserService(IServiceDependencies dependencies, IRepositoryManager repositoryManager) : base(new ServiceContext(dependencies.UnitOfWork, repositoryManager)) { }

        public IList<User> GetBlogWriters(Blog targetBlog)
        {
            return Repositories.Users.GetBlogWriters(targetBlog.BlogId);
        }

    }
}
