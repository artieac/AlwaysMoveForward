using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class DataMapManager
    {
        private static DataMapManager _dataMapManager = null;

        public static DataMapManager Mappers()
        {
            if (_dataMapManager == null)
            {
                _dataMapManager = new DataMapManager();
            }

            return _dataMapManager;
        }

        private BlogDataMap blogDataMap = null;
        public BlogDataMap BlogDataMap
        {
            get
            {
                if (blogDataMap == null)
                {
                    blogDataMap = new BlogDataMap();
                }

                return blogDataMap;
            }
        }

        private ListDataMap listDataMap = null;
        public ListDataMap ListDataMap
        {
            get
            {
                if (listDataMap == null)
                {
                    listDataMap = new ListDataMap();
                }

                return listDataMap;
            }
        }

        private BlogUserDataMap blogUserDataMap = null;
        public BlogUserDataMap BlogUserDataMap
        {
            get
            {
                if (blogUserDataMap == null)
                {
                    blogUserDataMap = new BlogUserDataMap();
                }

                return blogUserDataMap;
            }
        }

        private RoleDataMap roleDataMap = null;
        public RoleDataMap RoleDataMap
        {
            get
            {
                if (roleDataMap == null)
                {
                    roleDataMap = new RoleDataMap();
                }

                return roleDataMap;
            }
        }

        private UserDataMap userDataMap = null;
        public UserDataMap UserDataMap
        {
            get
            {
                if (userDataMap == null)
                {
                    userDataMap = new UserDataMap();
                }

                return userDataMap;
            }
        }

        private BlogPostDataMap blogPostDataMap = null;
        public BlogPostDataMap BlogPostDataMap
        {
            get
            {
                if (blogPostDataMap == null)
                {
                    blogPostDataMap = new BlogPostDataMap();
                }

                return blogPostDataMap;
            }
        }

        private CommentDataMap commentDataMap = null;
        public CommentDataMap CommentDataMap
        {
            get
            {
                if (commentDataMap == null)
                {
                    commentDataMap = new CommentDataMap();
                }

                return commentDataMap;
            }
        }

        private SiteInfoDataMap siteInfoDataMap = null;
        public SiteInfoDataMap SiteInfoDataMap
        {
            get
            {
                if (siteInfoDataMap == null)
                {
                    siteInfoDataMap = new SiteInfoDataMap();
                }

                return siteInfoDataMap;
            }
        }

        private TagDataMap tagDataMap = null;
        public TagDataMap TagDataMap
        {
            get
            {
                if (tagDataMap == null)
                {
                    tagDataMap = new TagDataMap();
                }

                return tagDataMap;
            }
        }

        private DbInfoMapper dbInfoMapper = null;
        public DbInfoMapper DbInfoMapper
        {
            get
            {
                if (dbInfoMapper == null)
                {
                    dbInfoMapper = new DbInfoMapper();
                }

                return dbInfoMapper;
            }
        }

    }
}
