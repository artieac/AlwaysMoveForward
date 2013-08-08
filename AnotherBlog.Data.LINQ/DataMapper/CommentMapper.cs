using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.DataMapper
{
    public class CommentMapper : DataMapper<Comment, EntryCommentDTO, IComment>
    {
    }
}
