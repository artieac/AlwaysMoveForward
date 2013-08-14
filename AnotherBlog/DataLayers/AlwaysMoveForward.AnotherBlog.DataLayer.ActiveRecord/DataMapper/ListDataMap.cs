using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class ListDataMap : DataMapBase<BlogList, BlogListDTO>
    {
        public override BlogListDTO MapProperties(BlogList source, BlogListDTO destination)
        {
            if (source != null)
            {
                if (destination == null)
                {
                    destination = new BlogListDTO();
                }

                destination.Id = source.Id;
                destination.Name = source.Name;
                destination.ShowOrdered = source.ShowOrdered;
            }

            return destination;
        }

        public override BlogList MapProperties(BlogListDTO source, BlogList destination)
        {
            if (source != null)
            {
                if (destination == null)
                {
                    destination = new BlogList();
                }

                destination.Id = source.Id;
                destination.Name = source.Name;
                destination.ShowOrdered = source.ShowOrdered;
            }

            return destination;
        }

        public override BlogList Map(BlogListDTO source)
        {
            BlogList retVal = this.MapProperties(source, null);
            retVal.Items = this.Map(retVal, source.Items);
            return retVal;
        }

        public BlogListDTO Map(BlogList source, BlogListDTO destination)
        {
            destination = this.MapProperties(source, destination);

            destination.Blog = DataMapManager.Mappers().BlogDataMap.MapProperties(source.Blog, destination.Blog);

            if (source.Items != null)
            {
                // First add in any new ones, and update any existing ones
                for (int i = 0; i < source.Items.Count; i++)
                {
                    BlogListItemDTO targetItem = destination.Items.FirstOrDefault(listItem => listItem.Id == source.Items[i].Id);

                    if (targetItem == null)
                    {
                        targetItem = this.Map(destination, source.Items[i]);
                        destination.Items.Add(targetItem);
                    }
                    else
                    {
                        targetItem = this.Map(destination, source.Items[i], targetItem);
                    }
                }
            }
            else
            {
                destination.Items = new List<BlogListItemDTO>();
            }

            // next remove any deleted ones
            for (int i = destination.Items.Count - 1; i > -1; i--)
            {
                BlogListItem targetItem = source.Items.FirstOrDefault(listItem => listItem.Id == destination.Items[i].Id);

                if (targetItem == null)
                {
                    destination.Items.RemoveAt(i);
                }
            }

            return destination;
        }

        private BlogListItemDTO Map(BlogListDTO owner, BlogListItem source)
        {
            return this.Map(owner, source, new BlogListItemDTO());
        }

        private BlogListItemDTO Map(BlogListDTO owner, BlogListItem source, BlogListItemDTO destination)
        {
            destination.BlogList = owner;
            destination.DisplayOrder = source.DisplayOrder;
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.RelatedLink = source.RelatedLink;

            return destination;
        }

        private IList<BlogListItem> Map(BlogList owner, IList<BlogListItemDTO> source)
        {
            IList<BlogListItem> retVal = new List<BlogListItem>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    BlogListItem newItem = new BlogListItem();
                    newItem.BlogList = owner;
                    newItem.DisplayOrder = source[i].DisplayOrder;
                    newItem.Id = source[i].Id;
                    newItem.Name = source[i].Name;
                    newItem.RelatedLink = source[i].RelatedLink;
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }

        private IList<BlogListItemDTO> Map(BlogListDTO owner, IList<BlogListItem> source)
        {
            IList<BlogListItemDTO> retVal = new List<BlogListItemDTO>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    BlogListItemDTO newItem = new BlogListItemDTO();
                    newItem.BlogList = owner;
                    newItem.DisplayOrder = source[i].DisplayOrder;
                    newItem.Id = source[i].Id;
                    newItem.Name = source[i].Name;
                    newItem.RelatedLink = source[i].RelatedLink;
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }

    }
}
