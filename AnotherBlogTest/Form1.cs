using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AnotherBlogTest.Services;
using AnotherBlogTest.WebServiceTests;

namespace AnotherBlogTest
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
//            tagTests.SetUp();
//            tagTests.GetAll();
//            tagTests.GetAllWithCount();
//            tagTests.GetByName();
//            tagTests.GetByNames();
//            tagTests.TearDown();

            BlogTest blogTests = new BlogTest();
//            blogTests.Setup();
//            blogTests.GetBySubFolder();
//            blogTests.GetByUserId();
//            blogTests.GetDefaultBlog();
//            blogTests.TearDown();

            BlogRollTest blogRollTests = new BlogRollTest();
//            blogRollTests.Setup();
//            blogRollTests.GetAllByBlog();
//            blogRollTests.TearDown();

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
            serviceTests.Setup();
            serviceTests.GetBlogEntriesTest();
            serviceTests.TearDown();
        }
    }
}
