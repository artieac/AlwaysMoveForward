using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.TagTalk.Common.DomainModel
{
    public class Comment
    {
        public int Id { get; set; }

        public IdentityTag IdentityTag { get; set; }

        public string CommentText { get; set; }

        public User Commentor { get; set; }

        public IdentityTag CommentorTag { get; set; }
    }
}
