using System;
using System.Collections.Generic;
using System.Linq;
using AlwaysMoveForward.OAuth2.Common.Utilities;

namespace AlwaysMoveForward.OAuth2.Web.Models
{
    public class PagedListModel<TListData>
    {
        public static int PageSize = 10;

        public PagedListModel(IList<TListData> source, int page)
        {
            IPagedList<TListData> pagedList = new PagedList<TListData>(source, page, PagedListModel<TListData>.PageSize);
            this.Paging = new PagingModel<TListData>(pagedList);
            this.Results = pagedList;
        }

        public PagingModel<TListData> Paging { get; set; }
        public IList<TListData> Results { get; set; }
    }
}