using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class AnotherBlogService
    {
        public AnotherBlogService(IUnitOfWork unitOfWork, IAnotherBlogRepositoryManager repositoryManager)
        {
            this.UnitOfWork = unitOfWork;
            this.AnotherBlogRepositories = repositoryManager;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public IAnotherBlogRepositoryManager AnotherBlogRepositories { get; set; }
    }
}
