using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class BlogDataMap : DataMapBase<Blog, BlogDTO>
    {
        #region Blog Aggregate root

        public override Blog MapProperties(BlogDTO source, Blog destination)
        {
            Blog retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new Blog();
                }

                retVal.About = source.About;
                retVal.BlogId = source.BlogId;
                retVal.ContactEmail = source.ContactEmail;
                retVal.Description = source.Description;
                retVal.Name = source.Name;
                retVal.SubFolder = source.SubFolder;
                retVal.Theme = source.Theme;
                retVal.WelcomeMessage = source.WelcomeMessage;
            }

            return retVal;
        }

        public override BlogDTO MapProperties(Blog source, BlogDTO destination)
        {
            BlogDTO retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new BlogDTO();
                }

                retVal.About = source.About;
                retVal.BlogId = source.BlogId;
                retVal.ContactEmail = source.ContactEmail;
                retVal.Description = source.Description;
                retVal.Name = source.Name;
                retVal.SubFolder = source.SubFolder;
                retVal.Theme = source.Theme;
                retVal.WelcomeMessage = source.WelcomeMessage;
            }

            return retVal;
        }

        public override Blog Map(BlogDTO source)
        {
            Blog retVal = null;

            if (source != null)
            {
                retVal = this.Map(source, null);
            }

            return retVal;
        }

        public Blog Map(BlogDTO source, Blog destination)
        {
            destination = this.MapProperties(source, destination);
            destination.Posts = DataMapManager.Mappers().BlogPostDataMap.Map(destination, source.Posts);
            return destination;
        }

        public override BlogDTO Map(Blog source)
        {
            BlogDTO retVal = null;

            if (source != null)
            {
                retVal = this.Map(source, null);
            }

            return retVal;
        }

        public BlogDTO Map(Blog source, BlogDTO destination)
        {
            destination = this.MapProperties(source, destination);
            destination.Posts = DataMapManager.Mappers().BlogPostDataMap.Map(destination, source.Posts);
            return destination;
        }

        #endregion

        #region BlogUser Aggregate Root

        public Blog Map(BlogUser owner, BlogDTO source)
        {
            Blog retVal = this.MapProperties(source, null);

            if (retVal != null)
            {
                retVal.Users = new List<BlogUser>();
                retVal.Users.Add(owner);
            }

            return retVal;
        }

        public BlogDTO Map(BlogUserDTO owner, Blog source)
        {
            BlogDTO retVal = this.MapProperties(source, null);

            if (retVal != null)
            {
                retVal.Users = new List<BlogUserDTO>();
                retVal.Users.Add(owner);
            }

            return retVal;
        }
        #endregion

        #region BlogPost AggregateRoot

        public Blog Map(BlogPost owner, BlogDTO source)
        {
            Blog retVal = this.MapProperties(source, null);

            if (retVal != null)
            {
                retVal.Posts = new List<BlogPost>();
                retVal.Posts.Add(owner);
            }

            return retVal;
        }

        public BlogDTO Map(BlogPostDTO owner, Blog source)
        {
            BlogDTO retVal = this.MapProperties(source, null);

            if (retVal != null)
            {
                retVal.Posts = new List<BlogPostDTO>();
                retVal.Posts.Add(owner);
            }

            return retVal;
        }

        #endregion
    }
}
