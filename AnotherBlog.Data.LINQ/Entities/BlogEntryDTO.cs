using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.LINQ.DataMapper;

namespace AnotherBlog.Data.LINQ.Entities
{
    public partial class BlogEntryDTO : IBlogPost
    {
        public Blog Blog
        {
            get { return BlogMapper.GetInstance().Map(this.BlogDTO); }
            set { this.BlogDTO = BlogMapper.GetInstance().Map(value); }
        }

        public User Author
        {
            get { return UserMapper.GetInstance().Map(this.UserDTO); }
            set { this.UserDTO = UserMapper.GetInstance().Map(value); }
        }

        public int GetCommentCount()
        {
            int retVal = 0;

            if (this.EntryCommentDTOs != null)
            {
                retVal = this.EntryCommentDTOs.Count();
            }

            return retVal;
        }
    }
}
