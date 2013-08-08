using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class BlogPostDataMap : DataMapBase<BlogPost, BlogPostDTO>
    {
        public override BlogPost MapProperties(BlogPostDTO source, BlogPost destination)
        {
            BlogPost retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new BlogPost();
                }

                retVal.DateCreated = source.DateCreated;
                retVal.DatePosted = source.DatePosted;
                retVal.EntryId = source.EntryId;
                retVal.EntryText = source.EntryText;
                retVal.IsPublished = source.IsPublished;
                retVal.TimesViewed = source.TimesViewed;
                retVal.Title = source.Title;
            }

            return retVal;
        }

        public override BlogPostDTO MapProperties(BlogPost source, BlogPostDTO destination)
        {
            BlogPostDTO retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new BlogPostDTO();
                }

                retVal.DateCreated = source.DateCreated;
                retVal.DatePosted = source.DatePosted;
                retVal.EntryId = source.EntryId;
                retVal.EntryText = source.EntryText;
                retVal.IsPublished = source.IsPublished;
                retVal.TimesViewed = source.TimesViewed;
                retVal.Title = source.Title;
            }

            return retVal;
        }

        public override BlogPost Map(BlogPostDTO source)
        {
            BlogPost retVal = null;

            if (source != null)
            {
                retVal = this.Map(source, null);
            }

            return retVal;
        }

        public BlogPost Map(BlogPostDTO source, BlogPost destination)
        {
            destination = this.MapProperties(source, destination);

            destination.Blog = DataMapManager.Mappers().BlogDataMap.Map(destination, source.Blog);
            destination.Author = DataMapManager.Mappers().UserDataMap.Map(source.Author);
            destination.Tags = DataMapManager.Mappers().TagDataMap.Map(destination, source.Tags);

            return destination;
        }

        public override BlogPostDTO Map(BlogPost source)
        {
            BlogPostDTO retVal = null;

            if (source != null)
            {
                retVal = this.Map(source, null);
            }

            return retVal;
        }

        public BlogPostDTO Map(BlogPost source, BlogPostDTO destination)
        {
            destination = this.MapProperties(source, destination);

            destination.Blog = DataMapManager.Mappers().BlogDataMap.Map(destination, source.Blog);
            destination.Author = DataMapManager.Mappers().UserDataMap.Map(source.Author);

            return destination;
        }

        #region Blog Aggregate Root

        public IList<BlogPost> Map(Blog owner, IList<BlogPostDTO> source)
        {
            IList<BlogPost> retVal = new List<BlogPost>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    BlogPost newItem = this.MapProperties(source[i], null);
                    newItem.Tags = DataMapManager.Mappers().TagDataMap.Map(newItem, source[i].Tags);
                    newItem.Blog = owner;
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }

        public IList<BlogPostDTO> Map(BlogDTO owner, IList<BlogPost> source)
        {
            IList<BlogPostDTO> retVal = new List<BlogPostDTO>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    BlogPostDTO newItem = this.MapProperties(source[i], null);
                    newItem.Blog = owner;
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }

        #endregion
        #region Comment aggregate root

        public BlogPost Map(Blog ownerBlog, Comment owner, BlogPostDTO source)
        {
            BlogPost retVal = this.MapProperties(source, null);
            retVal.Blog = ownerBlog;
            retVal.Author = DataMapManager.Mappers().UserDataMap.Map(source.Author);
            return retVal;
        }

        public BlogPostDTO Map(BlogDTO ownerBlog, EntryCommentsDTO owner, BlogPost source)
        {
            BlogPostDTO retVal = this.MapProperties(source, null);
            retVal.Blog = ownerBlog;
            retVal.Author = DataMapManager.Mappers().UserDataMap.Map(source.Author);
            return retVal;
        }
        #endregion
    }
}
