using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class TagService: ServiceBase
    {
        public TagService(ModelContext managerContext)
            : base(managerContext)
        {

        }

        public List<Tag> GetAll(Blog targetBlog)
        {
            TagGateway gateway = new TagGateway(this.ModelContext.DataContext);
            return gateway.GetAll(targetBlog.BlogId);
        }

        public List<TagCount> GetAllWithCount(Blog targetBlog)
        {
            TagGateway gateway = new TagGateway(this.ModelContext.DataContext);
            return gateway.GetAllWithCount(targetBlog.BlogId);
        }

        public List<Tag> GetByEntryId(int entryId)
        {
            TagGateway gateway = new TagGateway(this.ModelContext.DataContext);
            return gateway.GetByEntryId(entryId);
        }

        public Tag GetByName(string name, Blog targetBlog)
        {
            TagGateway gateway = new TagGateway(this.ModelContext.DataContext);
            return gateway.GetByName(name, targetBlog.BlogId);
        }

        public List<Tag> GetByNames(string[] names, int blogId)
        {
            TagGateway gateway = new TagGateway(this.ModelContext.DataContext);
            return gateway.GetByNames(names, blogId);
        }

        public List<Tag> AddTags(Blog targetBlog, string[] names, bool _submitChanges)
        {
            TagGateway gateway = new TagGateway(this.ModelContext.DataContext);

            List<Tag> retVal = new List<Tag>();

            for (int i = 0; i < names.Length; i++)
            {
                string trimmedName = names[i].Trim();

                if (trimmedName != String.Empty)
                {
                    Tag currentTag = gateway.GetByName(trimmedName, targetBlog.BlogId);

                    if (currentTag == null)
                    {
                        currentTag = new Tag();
                        currentTag.name = trimmedName;
                        currentTag.BlogId = targetBlog.BlogId;
                        gateway.Save(currentTag, false);
                    }

                    retVal.Add(currentTag);
                }
            }
            if (_submitChanges == true)
            {
                gateway.SubmitChanges();
            }

            return retVal;
        }
    }
}
