using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class CommentDataMap : DataMapBase<Comment, EntryCommentsDTO>
    {
        public override Comment MapProperties(EntryCommentsDTO source, Comment destination)
        {
            Comment retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new Comment();
                }

                retVal.AuthorEmail = source.AuthorEmail;
                retVal.AuthorName = source.AuthorName;
                retVal.CommentId = source.CommentId;
                retVal.DatePosted = source.DatePosted;
                retVal.PostId = source.PostId;
                retVal.Link = source.Link;
                retVal.Status = (Comment.CommentStatus)source.Status;
                retVal.Text = source.Text;
            }

            return retVal;
        }

        public override EntryCommentsDTO MapProperties(Comment source, EntryCommentsDTO destination)
        {
            EntryCommentsDTO retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new EntryCommentsDTO();
                }

                retVal.AuthorEmail = source.AuthorEmail;
                retVal.AuthorName = source.AuthorName;
                retVal.CommentId = source.CommentId;
                retVal.DatePosted = source.DatePosted;
                retVal.PostId = source.PostId;
                retVal.Link = source.Link;
                retVal.Status = (int)source.Status;
                retVal.Text = source.Text;
            }

            return retVal;
        }

        public IList<Comment> Map(BlogPost owner, IList<EntryCommentsDTO> source)
        {
            IList<Comment> retVal = new List<Comment>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    Comment newItem = this.MapProperties(source[i], null);
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }

        public EntryCommentsDTO Map(BlogPostDTO owner, Comment source, EntryCommentsDTO destination)
        {
            if (source != null)
            {
                destination = this.MapProperties(source, destination);
            }

            return destination;
        }
    }
}
