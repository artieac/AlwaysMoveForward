using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class DataMapManager
    {
        static DataMapManager()
        {
            AutoMapperConfiguration.Configure();
        }

        private static DataMapManager dataMapManager = null;

        public static DataMapManager Mappers()
        {
            if (DataMapManager.dataMapManager == null)
            {
                DataMapManager.dataMapManager = new DataMapManager();
            }

            return DataMapManager.dataMapManager;
        }

        private BlogDataMap blogDataMap = null;
        public BlogDataMap BlogDataMap
        {
            get
            {
                if (this.blogDataMap == null)
                {
                    this.blogDataMap = new BlogDataMap();
                }

                return this.blogDataMap;
            }
        }

        private ListDataMap listDataMap = null;
        public ListDataMap ListDataMap
        {
            get
            {
                if (this.listDataMap == null)
                {
                    this.listDataMap = new ListDataMap();
                }

                return this.listDataMap;
            }
        }

        private BlogUserDataMap blogUserDataMap = null;
        public BlogUserDataMap BlogUserDataMap
        {
            get
            {
                if (this.blogUserDataMap == null)
                {
                    this.blogUserDataMap = new BlogUserDataMap();
                }

                return this.blogUserDataMap;
            }
        }

        private RoleDataMap roleDataMap = null;
        public RoleDataMap RoleDataMap
        {
            get
            {
                if (this.roleDataMap == null)
                {
                    this.roleDataMap = new RoleDataMap();
                }

                return this.roleDataMap;
            }
        }

        private UserDataMap userDataMap = null;
        public UserDataMap UserDataMap
        {
            get
            {
                if (this.userDataMap == null)
                {
                    this.userDataMap = new UserDataMap();
                }

                return this.userDataMap;
            }
        }

        private BlogPostDataMap blogPostDataMap = null;
        public BlogPostDataMap BlogPostDataMap
        {
            get
            {
                if (this.blogPostDataMap == null)
                {
                    this.blogPostDataMap = new BlogPostDataMap();
                }

                return this.blogPostDataMap;
            }
        }

        private CommentDataMap commentDataMap = null;
        public CommentDataMap CommentDataMap
        {
            get
            {
                if (this.commentDataMap == null)
                {
                    this.commentDataMap = new CommentDataMap();
                }

                return this.commentDataMap;
            }
        }

        private SiteInfoDataMap siteInfoDataMap = null;
        public SiteInfoDataMap SiteInfoDataMap
        {
            get
            {
                if (this.siteInfoDataMap == null)
                {
                    this.siteInfoDataMap = new SiteInfoDataMap();
                }

                return this.siteInfoDataMap;
            }
        }

        private TagDataMap tagDataMap = null;
        public TagDataMap TagDataMap
        {
            get
            {
                if (this.tagDataMap == null)
                {
                    this.tagDataMap = new TagDataMap();
                }

                return this.tagDataMap;
            }
        }

        private DbInfoMapper databaseInfoMapper = null;
        public DbInfoMapper DbInfoMapper
        {
            get
            {
                if (this.databaseInfoMapper == null)
                {
                    this.databaseInfoMapper = new DbInfoMapper();
                }

                return this.databaseInfoMapper;
            }
        }
    }
}
