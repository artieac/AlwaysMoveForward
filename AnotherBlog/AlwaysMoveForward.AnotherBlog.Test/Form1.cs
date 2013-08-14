using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AlwaysMoveForward.AnotherBlog.Test.Services;
using AlwaysMoveForward.AnotherBlog.Test.WebServiceTests;

namespace AlwaysMoveForward.AnotherBlog.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TagTest tagTests = new TagTest();
            tagTests.SetUp();
            tagTests.GetAll();
            tagTests.GetAllWithCount();
            tagTests.TearDown();

            BlogTest blogTests = new BlogTest();
//            blogTests.Setup();
//            blogTests.GetBySubFolder();
//            blogTests.GetByUserId();
//            blogTests.GetDefaultBlog();
//            blogTests.TearDown();

            BlogUserTest blogUserTests = new BlogUserTest();
//            blogUserTests.Setup();
//            blogUserTests.Create();
//            blogUserTests.DeleteUserBlog();
//            blogUserTests.GetUserBlog();
//            blogUserTests.GetUserBlogs();
//            blogUserTests.Save();
//            blogUserTests.TearDown();

            SiteInfoTest siteTests = new SiteInfoTest();
//            siteTests.SiteFunctions();

            EntryCommentTest commentTests = new EntryCommentTest();
//            commentTests.SetUp();
//            commentTests.AddComment();
//            commentTests.AddLoggedInComment();
//            commentTests.GetAllUnapprovedComments();
//            commentTests.ApproveComment();
//            commentTests.GetAllApprovedComments();
//            commentTests.DeleteComment();
//            commentTests.GetAllDeletedComments();
//            commentTests.FullDeleteComment();
//            commentTests.TearDown();

            BlogPostServiceTests serviceTests = new BlogPostServiceTests();
            //serviceTests.Setup();
            //serviceTests.GetBlogEntriesTest();
            //serviceTests.TearDown();
        }
    }
}
