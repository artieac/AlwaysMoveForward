using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class AnotherBlogUserService : UserService
    {
        public AnotherBlogUserService(IUnitOfWork unitOfWork, IUserRepository userRepository) : base(unitOfWork, userRepository) { }

        public IList<User> GetBlogWriters(Blog targetBlog)
        {
            return this.UserRepository.GetBlogWriters(targetBlog.BlogId);
        }
    }
}
