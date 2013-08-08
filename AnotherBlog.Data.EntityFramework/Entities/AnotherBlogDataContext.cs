using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.EntityFramework.Entities
{
    public class AnotherBlogDataContext : DbContext
    {
        public AnotherBlogDataContext(String connectionString) : base(connectionString) { }
        public AnotherBlogDataContext(System.Data.Common.DbConnection dbConnection) : base(dbConnection){}

        public DbSet<Blog> BlogDTOs { get; set; }
        public DbSet<BlogPost> BlogPostDTOs { get; set; }
        public DbSet<BlogExtension> BlogExtensionDTOs { get; set; }
        public DbSet<BlogRollLink> BlogRollLinkDTOs { get; set; }
        public DbSet<BlogUser> BlogUserDTOs { get; set; }
        public DbSet<Comment> CommentDTOs { get; set; }
        public DbSet<DbInfo> DbInfoDTOs { get; set; }
        public DbSet<ExtensionConfiguration> ExtensionConfigurationDTO { get; set; }
        public DbSet<PostTag> PostTagDTOs { get; set; }
        public DbSet<Role> RoleDTOs { get; set; }
        public DbSet<SiteInfo> SiteInfoDTOs { get; set; }
        public DbSet<Tag> TagDTOs { get; set; }
        public DbSet<User> UserDTOs { get; set; }

        protected override void OnModelCreating(System.Data.Entity.ModelConfiguration.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().MapSingleType().ToTable("Blogs");
            modelBuilder.Entity<Blog>().HasKey(b => b.BlogId);

            modelBuilder.Entity<BlogPost>().MapSingleType
                (
                    blogPost => new
                    {
                        EntryId = blogPost.EntryId,
                        EntryText = blogPost.EntryText,
                        BlogId = blogPost.Blog.BlogId,
                        UserId = blogPost.Author.UserId,
                        Title = blogPost.Title,
                        IsPublished = blogPost.IsPublished,
                        DatePosted = blogPost.DatePosted,
                        DateCreated = blogPost.DateCreated
                    }
                ).ToTable("BlogEntries");
            modelBuilder.Entity<BlogPost>().HasKey(blogPost => blogPost.EntryId);

            modelBuilder.Entity<BlogExtension>().MapSingleType().ToTable("BlogExtensions");
            modelBuilder.Entity<BlogExtension>().HasKey(b => b.ExtensionId);

            modelBuilder.Entity<BlogRollLink>().MapSingleType
                (
                    blogRollLink => new
                    {
                        LinkName = blogRollLink.LinkName,
                        Url = blogRollLink.Url,
                        BlogId = blogRollLink.Blog.BlogId,
                        BlogRollLinkId = blogRollLink.BlogRollLinkId
                    }

                ).ToTable("BlogRollLinks");
            modelBuilder.Entity<BlogRollLink>().HasKey(b => b.BlogRollLinkId);

            modelBuilder.Entity<BlogUser>().MapSingleType
                (
                    blogUser => new
                    {
                        BlogUserId = blogUser.BlogUserId,
                        BlogId = blogUser.Blog.BlogId,
                        UserId = blogUser.User.UserId,
                        RoleId = blogUser.UserRole.RoleId
                    }
                ).ToTable("BlogUsers");
            modelBuilder.Entity<BlogUser>().HasKey(b => b.BlogUserId);

            modelBuilder.Entity<Comment>().MapSingleType
                 (
                    comment => new
                    {
                        CommentId = comment.CommentId,
                        EntryId = comment.Post.EntryId,
                        AuthorEmail = comment.AuthorEmail,
                        RelatedComment = 0,
                        BlogId = comment.Blog.BlogId,
                        Comment = comment.Text,
                        AuthorName = comment.AuthorName,
                        Status = comment.Status,
                        Link = comment.Link,
                        DatePosted = comment.DatePosted
                    }
                ).ToTable("EntryComments");
            modelBuilder.Entity<Comment>().HasKey(b => b.CommentId);
            
            modelBuilder.Entity<DbInfo>().MapSingleType().ToTable("DbInfo");
            modelBuilder.Entity<DbInfo>().HasKey(b => b.Version);
            
            modelBuilder.Entity<ExtensionConfiguration>().MapSingleType().ToTable("ExtensionConfiguration");
            modelBuilder.Entity<ExtensionConfiguration>().HasKey(b => b.ConfigurationId);

            modelBuilder.Entity<PostTag>().MapSingleType
                (
                    postTag => new
                    {
                        TagId = postTag.Tag.Id,
                        BlogEntryId = postTag.Post.EntryId,
                        BlogEntryTagId = postTag.PostTagId
                    }
                ).ToTable("BlogEntryTags");
            modelBuilder.Entity<PostTag>().HasKey(b => b.PostTagId);
            
            modelBuilder.Entity<Role>().MapSingleType().ToTable("Roles");
            modelBuilder.Entity<Role>().HasKey(b => b.RoleId);
            
            modelBuilder.Entity<SiteInfo>().MapSingleType().ToTable("SiteInfo");
            modelBuilder.Entity<SiteInfo>().HasKey(b => b.SiteId);

            modelBuilder.Entity<Tag>().MapSingleType
                (
                    tag => new
                    {
                        name = tag.Name,
                        id = tag.Id,
                        BlogId = tag.Blog.BlogId
                    }
                ).ToTable("Tags");     
            modelBuilder.Entity<Tag>().HasKey(b => b.Id);
            
            modelBuilder.Entity<User>().MapSingleType().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(b => b.UserId);
        }

        public DbSet<TableDTO> GetTable<TableDTO>() where TableDTO : class
        {
            DbSet<TableDTO> retVal = null;

            Type targetType = typeof(TableDTO);

            if (targetType == typeof(Blog))
            {
                retVal = this.BlogDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(BlogPost))
            {
                retVal = this.BlogPostDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(BlogExtension))
            {
                retVal = this.BlogExtensionDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(BlogRollLink))
            {
                retVal = this.BlogRollLinkDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(BlogUser))
            {
                retVal = this.BlogUserDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(Comment))
            {
                retVal = this.CommentDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(DbInfo))
            {
                retVal = this.DbInfoDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(ExtensionConfiguration))
            {
                retVal = this.ExtensionConfigurationDTO as DbSet<TableDTO>;
            }
            else if (targetType == typeof(PostTag))
            {
                retVal = this.PostTagDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(Role))
            {
                retVal = this.RoleDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(SiteInfo))
            {
                retVal = this.SiteInfoDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(Tag))
            {
                retVal = this.TagDTOs as DbSet<TableDTO>;
            }
            else if (targetType == typeof(User))
            {
                retVal = this.UserDTOs as DbSet<TableDTO>;
            }

            return retVal;
        }

        public IEnumerable<DTOType> CreateQuery<DTOType>(String queryString)
        {
            IEnumerable<DTOType> retVal = null;

            System.Data.Linq.DataContext dc = new System.Data.Linq.DataContext(this.Database.Connection.ConnectionString);
            retVal = dc.ExecuteQuery<DTOType>(queryString);
            
            return retVal;
        }

        public IEnumerable<DTOType> CreateQuery<DTOType>(String queryString, object[] queryParams)
        {
            IEnumerable<DTOType> retVal = null;

            System.Data.Linq.DataContext dc = new System.Data.Linq.DataContext(this.Database.Connection.ConnectionString);
            retVal = dc.ExecuteQuery<DTOType>(queryString, queryParams);

            return retVal;
        }

        private String GenerateEFConnectionString()
        {
            string connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder
            {
                Metadata = "res://*",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = new System.Data.SqlClient.SqlConnectionStringBuilder(this.Database.Connection.ConnectionString).ConnectionString
            }.ConnectionString;

            return connectionString;
        }
    }
}
