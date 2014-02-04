using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class BlogList 
    {
        public BlogList()
        {
            this.Id = -1;
        }

        public virtual int Id { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual string Name{ get; set;}
        public virtual bool ShowOrdered { get; set; }
        public virtual IList<BlogListItem> Items { get; set; }

        public bool RemoveListItem(int listItemId)
        {
            bool retVal = false;

            BlogListItem targetItem = this.Items.FirstOrDefault(t => t.Id == listItemId);

            if (targetItem != null)
            {
                retVal = this.RemoveListItem(targetItem);
            }

            return retVal;
        }

        public bool RemoveListItem(BlogListItem listItem)
        {
            bool retVal = false;

            if (listItem != null)
            {
                this.Items.Remove(listItem);
                retVal = true;
            }

            return retVal;
        }
    }
}
