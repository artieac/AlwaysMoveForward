using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public interface IBlogPost
    {
        int EntryId { get; set; }
        bool IsPublished { get; set; }
        Blog Blog { get; set; }
        User Author { get; set; }
        string EntryText { get; set; }
        string Title { get; set; }
        DateTime DatePosted { get; set; }
        DateTime DateCreated { get; set; }

        int GetCommentCount();
    }
}
