using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

using TheOffWing.AnotherBlog.Core;
using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// This class contains all the code to extract Tag data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class TagGateway : GatewayBase
    {
        public TagGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// Get all tags related to a specific blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<Tag> GetAll(int blogId)
        {
            IQueryable<Tag> retVal = from foundItem in this.DataContext.Tags where foundItem.BlogId==blogId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Get all tags related to a specific blog entry.
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public PagedList<Tag> GetByEntryId(int entryId)
        {
            IQueryable<Tag> retVal = from foundItem in this.DataContext.Tags
                                     join entryTag in this.DataContext.BlogEntryTags on foundItem.id equals entryTag.TagId
                                     where entryTag.BlogEntryId==entryId
                                     select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Get a specific tag.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public Tag GetByName(string name, int blogId)
        {
            Tag retVal = null;

            try
            {
                Table<Tag> dataTable = this.DataContext.GetTable<Tag>();
                retVal = (from foundItem in this.DataContext.Tags where foundItem.name == name && foundItem.BlogId == blogId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get multiple tag records.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<Tag> GetByNames(string[] names, int blogId)
        {
            IQueryable<Tag> retVal = from foundItem in this.DataContext.Tags
                                      where names.Contains(foundItem.name) && foundItem.BlogId==blogId
                                     select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Save a tag to the database if it doesn't exist yet, otherwise do nothing.
        /// </summary>
        /// <param name="newTag"></param>
        /// <param name="_submitChanges"></param>
        public void Save(Tag newTag, bool _submitChanges)
        {
            this.DataContext.Tags.InsertOnSubmit(newTag);

            if (_submitChanges == true)
            {
                this.SubmitChanges();
            }
        }
    }
}
