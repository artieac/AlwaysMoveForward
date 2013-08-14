using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.AnotherBlog.Web.Code.Extensions
{
    public class TabElements
    {
        public TabElements()
        {
            this.TabItems = new List<TabItem>();
        }

        public class TabItem
        {
            public String Title { get; set; }
            public String TargetUrl { get; set; }
            public String Image { get; set; }
        }

        public int SelectedTab { get; set; }
        public List<TabItem> TabItems { get; set; }

        public void SetSelectedTab(Uri currentUrl)
        {
            for (int i = 0; i < TabItems.Count; i++)
            {
                if (currentUrl.ToString().Contains(TabItems[i].TargetUrl))
                {
                    this.SelectedTab = i;
                    break;
                }
            }
        }

        public void Add(String title, String targetUrl, String image)
        {
            TabItem newTab = new TabItem();
            newTab.Title = title;
            newTab.TargetUrl = targetUrl;
            newTab.Image = image;
            TabItems.Add(newTab);
        }
    }
}