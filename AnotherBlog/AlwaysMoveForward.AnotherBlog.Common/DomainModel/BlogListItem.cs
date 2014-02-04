using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class BlogListItem 
    {
        public BlogListItem()
        {
            this.Id = -1;
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string RelatedLink { get; set; }
        public virtual int DisplayOrder { get; set; }
    }
}
