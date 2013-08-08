using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    /// <summary>
    /// A class to manage the business rules between a blog entry and its tags.
    /// </summary>
    public class BlogEntryTagService : ServiceBase
    {
        public BlogEntryTagService(ModelContext managerContext)
            : base(managerContext)
        {

        }
        /// <summary>
        /// Create and initialize a BlogEntryTag instance
        /// </summary>
        /// <returns></returns>
        public BlogEntryTag Create()
        {
            return new BlogEntryTag();
        }
        /// <summary>
        /// Relate existing tags to an existing blog entry
        /// </summary>
        /// <param name="blogEntry"></param>
        /// <param name="tagsToAssociate"></param>
        /// <param name="_submitChanges"></param>
        public void AssociateTags(BlogEntry blogEntry, List<Tag> tagsToAssociate, bool _submitChanges)
        {
            BlogEntryTagGateway gateway = new BlogEntryTagGateway(this.ModelContext.DataContext);

            List<BlogEntryTag> blogEntryTags = gateway.GetByBlogEntryId(blogEntry.EntryId);

            // There has got to be a better way to do this something like
            // 1. Is there a way to use .Any? 
            // 2. Use an IEqualityComparitor?
            // 3. Just delete all associations for the blog entry and re insert?
            // Not sure the best way for 1 or 2, and not sure of the LINQ ramifications for 3 so
            // for not just get it working the hard way.
            for(int i = 0; i < blogEntryTags.Count; i++)
            {
                bool matchFound = false;

                for(int j = 0; j < tagsToAssociate.Count; j++)
                {
                    if (blogEntryTags[i].TagId == tagsToAssociate[j].id)
                    {
                        // Tag already related, match found bounce out
                        matchFound = true;
                    }
                }

                if (matchFound == false)
                {
                    gateway.Delete(blogEntryTags[i]);
                }
            }

            for (int i = 0; i < tagsToAssociate.Count; i++)
            {
                bool matchFound = false;

                for (int j = 0; j < blogEntryTags.Count; j++)
                {
                    if (tagsToAssociate[i].id == blogEntryTags[j].TagId)
                    {
                        matchFound = true;
                    }
                }

                if (matchFound == false)
                {
                    BlogEntryTag newTag = this.Create();
                    newTag.TagId = tagsToAssociate[i].id;
                    newTag.BlogEntryId = blogEntry.EntryId;

                    gateway.Save(newTag);
                }
            }

            if (_submitChanges == true)
            {
                this.ModelContext.DataContext.DataContext.SubmitChanges();
            }
        }
        /// <summary>
        /// Get all tag relationships for a give blog entry
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public List<BlogEntryTag> GetByBlogEntryId(int entryId)
        {
            BlogEntryTagGateway gateway = new BlogEntryTagGateway(this.ModelContext.DataContext);
            return gateway.GetByBlogEntryId(entryId);
        }
    }
}
