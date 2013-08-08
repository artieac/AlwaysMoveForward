using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public class DataMapper<DomainClass, DTOClass, CommonInterface> where DomainClass : class, CommonInterface where DTOClass : class, CommonInterface
    {
        static DataMapper<DomainClass, DTOClass, CommonInterface> mapperInstance;

        public static DataMapper<DomainClass, DTOClass, CommonInterface> GetInstance()
        {
            if (mapperInstance == null)
            {
                mapperInstance = new DataMapper<DomainClass, DTOClass, CommonInterface>();
            }

            return mapperInstance;
        }

        public DataMapper() { }

        public DomainClass Map(DTOClass source)
        {
            DomainClass retVal = null;
            
            if (source != null)
            {
                retVal = Activator.CreateInstance<DomainClass>();

                if (retVal is CommonInterface && source is CommonInterface)
                {
                    PropertyInfo[] interfaceProperties = typeof(CommonInterface).GetProperties();

                    for (int i = 0; i < interfaceProperties.Count(); i++)
                    {
                        interfaceProperties[i].SetValue(retVal, interfaceProperties[i].GetValue(source, null), null);
                    }
                }
            }

            return retVal;
        }

        public IList<DomainClass> Map(IList<DTOClass> source)
        {
            IList<DomainClass> retVal = new List<DomainClass>();

            if (source != null)
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }

        public DTOClass Map(DomainClass source)
        {
            DTOClass retVal = null;

            if (source != null)
            {
                retVal = Activator.CreateInstance<DTOClass>();

                if (retVal is CommonInterface && source is CommonInterface)
                {
                    PropertyInfo[] interfaceProperties = typeof(CommonInterface).GetProperties();

                    for (int i = 0; i < interfaceProperties.Count(); i++)
                    {
                        interfaceProperties[i].SetValue(retVal, interfaceProperties[i].GetValue(source, null), null);
                    }
                }
            }

            return retVal;
        }

        public IList<DTOClass> Map(IList<DomainClass> source)
        {
            IList<DTOClass> retVal = new List<DTOClass>();

            if (source != null)
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }

        public CommonInterface IMap(DTOClass source)
        {
            DomainClass retVal = null;

            if (source != null)
            {
                retVal = Activator.CreateInstance<DomainClass>();

                if (retVal is CommonInterface && source is CommonInterface)
                {
                    PropertyInfo[] interfaceProperties = typeof(CommonInterface).GetProperties();

                    for (int i = 0; i < interfaceProperties.Count(); i++)
                    {
                        interfaceProperties[i].SetValue(retVal, interfaceProperties[i].GetValue(source, null), null);
                    }
                }
            }

            return retVal;
        }

        public IList<CommonInterface> IMap(IList<DTOClass> source)
        {
            IList<CommonInterface> retVal = new List<CommonInterface>();

            if (source != null)
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }

        public CommonInterface IMap(DomainClass source)
        {
            DTOClass retVal = null;

            if (source != null)
            {
                retVal = Activator.CreateInstance<DTOClass>();

                if (retVal is CommonInterface && source is CommonInterface)
                {
                    PropertyInfo[] interfaceProperties = typeof(CommonInterface).GetProperties();

                    for (int i = 0; i < interfaceProperties.Count(); i++)
                    {
                        interfaceProperties[i].SetValue(retVal, interfaceProperties[i].GetValue(source, null), null);
                    }
                }
            }

            return retVal;
        }

        public IList<CommonInterface> IMap(IList<DomainClass> source)
        {
            IList<CommonInterface> retVal = new List<CommonInterface>();

            if (source != null)
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    retVal.Add(this.Map(source[i]));
                }
            }

            return retVal;
        }        //public static ReturnClass Map<ReturnClass>(IBlog source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IBlog && source != null)
        //    {
        //        ((IBlog)retVal).BlogId = source.BlogId;
        //        ((IBlog)retVal).About = source.About;
        //        ((IBlog)retVal).ContactEmail = source.ContactEmail;
        //        ((IBlog)retVal).Description = source.Description;
        //        ((IBlog)retVal).Name = source.Name;
        //        ((IBlog)retVal).SubFolder = source.SubFolder;
        //        ((IBlog)retVal).Theme = source.Theme;
        //        ((IBlog)retVal).WelcomeMessage = source.WelcomeMessage;
        //        ((IBlog)retVal).Users = source.Users;
        //        ((IBlog)retVal).Comments = source.Comments;
        //        ((IBlog)retVal).Posts = source.Posts;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IBlogExtension source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IBlogExtension && source != null)
        //    {
        //        ((IBlogExtension)retVal).AssemblyName = source.AssemblyName;
        //        ((IBlogExtension)retVal).AssemblyPath = source.AssemblyPath;
        //        ((IBlogExtension)retVal).ClassName = source.ClassName;
        //        ((IBlogExtension)retVal).ExtensionId = source.ExtensionId;
        //        ((IBlogExtension)retVal).PageLocation = source.PageLocation;
        //        ((IBlogExtension)retVal).SectionOrder = source.SectionOrder;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IBlogPost source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IBlogPost && source != null)
        //    {
        //        ((IBlogPost)retVal).Author = source.Author;
        //        ((IBlogPost)retVal).Blog = source.Blog;
        //        ((IBlogPost)retVal).Comments = source.Comments;
        //        ((IBlogPost)retVal).DateCreated = source.DateCreated;
        //        ((IBlogPost)retVal).DatePosted = source.DatePosted;
        //        ((IBlogPost)retVal).EntryId = source.EntryId;
        //        ((IBlogPost)retVal).EntryText = source.EntryText;
        //        ((IBlogPost)retVal).IsPublished = source.IsPublished;
        //        ((IBlogPost)retVal).Tags = source.Tags;
        //        ((IBlogPost)retVal).Title = source.Title;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IBlogRollLink source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IBlogRollLink && source != null)
        //    {
        //        ((IBlogRollLink)retVal).Blog = source.Blog;
        //        ((IBlogRollLink)retVal).BlogRollLinkId = source.BlogRollLinkId;
        //        ((IBlogRollLink)retVal).LinkName = source.LinkName;
        //        ((IBlogRollLink)retVal).Url = source.Url;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IBlogUser source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IBlogUser && source != null)
        //    {
        //        ((IBlogUser)retVal).Blog = source.Blog;
        //        ((IBlogUser)retVal).BlogUserId = source.BlogUserId;
        //        ((IBlogUser)retVal).User = source.User;
        //        ((IBlogUser)retVal).UserRole = source.UserRole;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass, SourceInterface>(SourceInterface source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is SourceInterface && source != null)
        //    {
        //        PropertyInfo[] interfaceProperties = source.GetType().GetProperties();

        //        for (int i = 0; i < interfaceProperties.Count(); i++)
        //        {
        //            interfaceProperties[i].SetValue(retVal, interfaceProperties[i].GetValue(source, null), null);
        //        }
        //    }

        //    return retVal;
        //}

        //public static IList<ReturnClass> Map<ReturnClass, SourceInterface>(IList<SourceInterface> source)
        //{
        //    IList<ReturnClass> retVal = new List<ReturnClass>();

        //    for (int i = 0; i < source.Count(); i++)
        //    {
        //        retVal.Add(DataMapper.Map<ReturnClass, SourceInterface>(((SourceInterface)source[i])));
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IComment source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IComment && source != null)
        //    {
        //        ((IComment)retVal).AuthorEmail = source.AuthorEmail;
        //        ((IComment)retVal).AuthorName = source.AuthorName;
        //        ((IComment)retVal).Blog = source.Blog;
        //        ((IComment)retVal).CommentId = source.CommentId;
        //        ((IComment)retVal).DatePosted = source.DatePosted;
        //        ((IComment)retVal).Link = source.Link;
        //        ((IComment)retVal).Post = source.Post;
        //        ((IComment)retVal).Status = source.Status;
        //        ((IComment)retVal).Text = source.Text;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IDbInfo source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IDbInfo && source != null)
        //    {
        //        ((IDbInfo)retVal).Version = source.Version;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IRole source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IRole && source != null)
        //    {
        //        ((IRole)retVal).Name = source.Name;
        //        ((IRole)retVal).RoleId = source.RoleId;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(ISiteInfo source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is ISiteInfo && source != null)
        //    {
        //        ((ISiteInfo)retVal).About = source.About;
        //        ((ISiteInfo)retVal).ContactEmail = source.ContactEmail;
        //        ((ISiteInfo)retVal).DefaultTheme = source.DefaultTheme;
        //        ((ISiteInfo)retVal).Name = source.Name;
        //        ((ISiteInfo)retVal).SiteAnalyticsId = source.SiteAnalyticsId;
        //        ((ISiteInfo)retVal).SiteId = source.SiteId;
        //        ((ISiteInfo)retVal).Url = source.Url;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(ITag source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is ITag != null && source != null)
        //    {
        //        ((ITag)retVal).Blog = source.Blog;
        //        ((ITag)retVal).BlogEntries = source.BlogEntries;
        //        ((ITag)retVal).Id = source.Id;
        //        ((ITag)retVal).Name = source.Name;
        //    }

        //    return retVal;
        //}

        //public static ReturnClass Map<ReturnClass>(IUser source)
        //{
        //    ReturnClass retVal = Activator.CreateInstance<ReturnClass>();

        //    if (retVal is IUser && source != null)
        //    {
        //        ((IUser)retVal).About = source.About;
        //        ((IUser)retVal).ApprovedCommenter = source.ApprovedCommenter;
        //        ((IUser)retVal).BlogEntries = source.BlogEntries;
        //        ((IUser)retVal).DisplayName = source.DisplayName;
        //        ((IUser)retVal).Email = source.Email;
        //        ((IUser)retVal).IsActive = source.IsActive;
        //        ((IUser)retVal).IsSiteAdministrator = source.IsSiteAdministrator;
        //        ((IUser)retVal).Password = source.Password;
        //        ((IUser)retVal).UserBlogs = source.UserBlogs;
        //        ((IUser)retVal).UserId = source.UserId;
        //        ((IUser)retVal).UserName = source.UserName;
        //    }

        //    return retVal;
        //}


    }
}
