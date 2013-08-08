using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnotherBlog.Core
{
    public class ServiceFactory
    {
        public enum Types
        {
            Blog,
            BlogEntry,
            BlogUser,
            Role,
            Tag,
            UploadedFile,
            User,
            EntryComment,
            BlogEntryTag,
            BlogLink
        };

        public static ServiceBase GetManager(Types managerType)
        {
            return ServiceFactory.GetManager(managerType, new ModelContext());
        }

        public static ServiceBase GetManager(Types managerType, ModelContext modelContext)
        {
            ServiceBase retVal = null;

            switch (managerType)
            {
                case Types.Blog:
                    retVal = new BlogService(modelContext);
                    break;
                case Types.BlogEntry:
                    retVal = new BlogEntryService(modelContext);
                    break;
                case Types.BlogEntryTag:
                    retVal = new BlogEntryTagService(modelContext);
                    break;
                case Types.BlogUser:
                    retVal = new BlogUserService(modelContext);
                    break;
                case Types.EntryComment:
                    retVal = new EntryCommentService(modelContext);
                    break;
                case Types.Role:
                    retVal = new RoleService(modelContext);
                    break;
                case Types.Tag:
                    retVal = new TagService(modelContext);
                    break;
                case Types.UploadedFile:
                    retVal = new UploadedFileManager(modelContext);
                    break;
                case Types.User:
                    retVal = new UserManager(modelContext);
                    break;
                case Types.BlogLink:
                    retVal = new BlogRollService(modelContext);
                    break;
            }

            return retVal;
        }
    }
}
