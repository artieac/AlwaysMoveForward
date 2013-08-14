using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class TagDataMap : DataMapBase<Tag, TagDTO>
    {
        public override Tag MapProperties(TagDTO source, Tag destination)
        {
            if (source != null)
            {
                if (destination == null)
                {
                    destination = new Tag();
                }

                destination.Id = source.Id;
                destination.Name = source.Name;
            }

            return destination;
        }

        public override TagDTO MapProperties(Tag source, TagDTO destination)
        {
            if (source != null)
            {
                if (destination == null)
                {
                    destination = new TagDTO();
                }

                destination.Id = source.Id;
                destination.Name = source.Name;
            }

            return destination;
        }

        public IList<Tag> Map(BlogPost owner, IList<TagDTO> source)
        {
            IList<Tag> retVal = new List<Tag>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    Tag newItem = this.MapProperties(source[i], null);
                    newItem.Blog = owner.Blog;
                    newItem.BlogEntries = new List<BlogPost>();
                    newItem.BlogEntries.Add(owner);
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }
    }
}
